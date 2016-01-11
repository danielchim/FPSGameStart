using UnityEngine;
using System.Collections;
using System;

namespace Gameplay
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private Vector3 playerOffset;
        [SerializeField]
        private float cameraSpeed = 5f;

        private Transform playerTransform;

        private GameplayController gameplayController;


        [SerializeField]
        private float moveSpeed = 1f;// How fast the rig will move to keep up with the target's position.
        [Range(0f, 10f)]
        [SerializeField]
        private float turnSpeed = 1.5f;// How fast the rig will rotate from user input.
        [SerializeField]
        private float turnSmoothing = 0.1f;// How much smoothing to apply to the turn input, to reduce mouse-turn jerkiness
        [SerializeField]
        private float tiltMax = 75f; // The maximum value of the x axis rotation of the pivot.
        [SerializeField]
        private float tiltMin = 45f; // The minimum value of the x axis rotation of the pivot.
        [SerializeField]
        private bool lockCursor = false; // Whether the cursor should be hidden and locked.
        [SerializeField]
        private bool verticalAutoReturn = false;// set wether or not the vertical axis should auto return

        private float lookAngle; // The rig's y axis rotation.
        private float tiltAngle; // The pivot's x axis rotation.
        private const float LookDistance = 100f; // How far in front of the pivot the character's look target is.
        private float smoothX = 0;
        private float smoothY = 0;
        private float smoothXvelocity = 0;
        private float smoothYvelocity = 0;


        private void Awake()
        {
            gameplayController = GameplayController.Instance;
        }

        private void OnEnable()
        {
            gameplayController.OnPlayerSpawnedEvent += OnPlayerSpawned;
        }

        private void OnDisable()
        {
            gameplayController.OnPlayerSpawnedEvent -= OnPlayerSpawned;
        }

        //quando faz quaquer operacao com camera melho fazer no lateUpdate
        private void LateUpdate()
        {
            if (playerTransform == null)
                return;

            UpdateCameraPosition();
        }

        private void OnPlayerSpawned(Unit.PlayerUnit player)
        {
            playerTransform = player.transform;
        }

        private void UpdateCameraPosition()
        {
            Vector3 finalPosition = playerTransform.position - playerOffset;
            transform.position = Vector3.Lerp(transform.position, finalPosition, cameraSpeed * Time.smoothDeltaTime);
        }



        private void HandleRotationMovement()
        {
            // Read the user input
            var x = Input.GetAxis("Mouse X");
            var y = Input.GetAxis("Mouse Y");

            // smooth the user input
            if (turnSmoothing > 0)
            {
                smoothX = Mathf.SmoothDamp(smoothX, x, ref smoothXvelocity, turnSmoothing);
                smoothY = Mathf.SmoothDamp(smoothY, y, ref smoothYvelocity, turnSmoothing);
            }
            else
            {
                smoothX = x;
                smoothY = y;
            }

            // Adjust the look angle by an amount proportional to the turn speed and horizontal input.
            lookAngle += smoothX * turnSpeed;

            // Rotate the rig (the root object) around Y axis only:
            transform.rotation = Quaternion.Euler(0f, lookAngle, 0f);

            if (verticalAutoReturn)
            {
                // For tilt input, we need to behave differently depending on whether we're using mouse or touch input:
                // on mobile, vertical input is directly mapped to tilt value, so it springs back automatically when the look input is released
                // we have to test whether above or below zero because we want to auto-return to zero even if min and max are not symmetrical.
                tiltAngle = y > 0 ? Mathf.Lerp(0, -tiltMin, smoothY) : Mathf.Lerp(0, tiltMax, -smoothY);
            }
            else
            {
                // on platforms with a mouse, we adjust the current angle based on Y mouse input and turn speed
                tiltAngle -= smoothY * turnSpeed;
                // and make sure the new value is within the tilt range
                tiltAngle = Mathf.Clamp(tiltAngle, -tiltMin, tiltMax);
            }
            
        }
    }
}
