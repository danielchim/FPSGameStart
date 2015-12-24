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
        private void Update()
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
    }
}
