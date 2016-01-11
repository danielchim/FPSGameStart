using UnityEngine;
using System.Collections;
using Gameplay.Unit.Movement;
using System;
using Gameplay.Attribute;

using Gameplay.Unit.Animation;

namespace Gameplay.Unit.Movement
{
    [RequireComponent(typeof(NewBaseUnit))]
    public class PlayerControllerMovement : BaseMovement
    {
        private Vector3 playerInput = Vector3.zero;

        [SerializeField] private LayerMask groundLayer;
        private Quaternion mouseRotation = Quaternion.identity;
        private Transform cam; // Uma referência para a câmera principal nas cenas Transform
        private Vector3 camForward; // A atual direção para a frente da câmera
        private Vector3 lookPos; // A posição que o personagem deve estar olhando para
        private Vector3 currentLookPos;
        public bool lookInCameraDirection = true;// o personagem deve estar olhando na mesma direção que a câmera está enfrentando

        public float autoTurnThresholdAngle = 100; // caráter auto vira para a direção da câmera de costas por mais do que este ângulo
        public float autoTurnSpeed = 2; // velocidade em que caráter auto-gira na direção cam

        private float turnAmount;
        private float forwardAmount;

        [SerializeField]
        private AdvancedSettings advancedSettings; // Container for the advanced settings class , thiss allows the advanced settings to be in a foldout in the inspector




        [System.Serializable]
        public class AdvancedSettings
        {
            public float stationaryTurnSpeed = 180; // additional turn speed added when the player is stationary (added to animation root rotation)
            public float movingTurnSpeed = 360; // additional turn speed added when the player is moving (added to animation root rotation)
            public float headLookResponseSpeed = 2; // speed at which head look follows its target
            public float crouchHeightFactor = 0.6f; // collider height is multiplied by this when crouching
            public float crouchChangeSpeed = 4; // speed at which capsule changes height when crouching/standing
            public float autoTurnThresholdAngle = 100; // character auto turns towards camera direction if facing away by more than this angle
            public float autoTurnSpeed = 2; // speed at which character auto-turns towards cam direction
            public PhysicMaterial zeroFrictionMaterial; // used when in motion to enable smooth movement
            public PhysicMaterial highFrictionMaterial; // used when stationary to avoid sliding down slopes
            public float jumpRepeatDelayTime = 0.25f; // amount of time that must elapse between landing and being able to jump again
            public float runCycleLegOffset = 0.2f; // animation cycle offset (0-1) used for determining correct leg to jump off
            public float groundStickyEffect = 5f; // power of 'stick to ground' effect - prevents bumping down slopes.
        }


        private void Start()
        {
            // get the transform of the main camera
            if (Camera.main != null)
            {
                cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.");
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            currentLookPos = Camera.main.transform.position;
        }

        private void FixedUpdate()
        {
            CheckInput();
            Move();
            ConvertMoveInput();
            ApplyExtraTurnRotation();
            Turn();
            SetFriction();
        }



        private void CheckInput()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");



            // seta valores para animacao
            PlayerAnimation.horizontalGetAxis = horizontal;
            PlayerAnimation.verticalAxis = vertical;

            playerInput = new Vector3(horizontal, 0, vertical);

            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(mouseRay, out hit, 100, groundLayer.value))
            {
                Vector3 diff = hit.point - transform.position;
                diff.y = 0;
                mouseRotation = Quaternion.LookRotation(diff);

                Debug.DrawRay(transform.position, diff, Color.green);
            }





            //Calcula direção de movimento para passar para personagem
            if (cam != null)
            {
                //Calcula câmera direção relativa para mover:
                camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
                playerInput = vertical * camForward + horizontal * cam.right;
            } else
            {
                //Usamos direções relativa do mundo no caso de nenhuma câmera principal
                playerInput = vertical * camForward + horizontal * cam.right;
            }


            //// Calcula o olhar posição de destino cabeça
            if (playerInput.magnitude > 1)
                playerInput.Normalize();

            lookPos = lookInCameraDirection && cam != null ? transform.position + cam.forward * 100 : transform.position + transform.forward * 100;

            currentLookPos = lookPos;





        }


        // Converte o movimento vector parente em valores turn & FWD locais
        private void ConvertMoveInput()
        {
            // Converter o mundo em relação vector movimento de entrada em um montante quantidade local com relativa 
            // volta e para a frente obrigados a cabeça na direção desejada
            Vector3 localMove = transform.InverseTransformDirection(playerInput);
            turnAmount = Mathf.Atan2(localMove.x, localMove.z);
            forwardAmount = localMove.z;
        }
        private void Turn()
        {
            rigidBodyPlayer.MoveRotation(mouseRotation);

            // Desliga-se automaticamente para enfrentar direção da câmera,
            // Quando não se movendo, e para além do limiar ângulo especificado

            if (Mathf.Abs(forwardAmount) < .01f)
            {
                Vector3 lookDelta = transform.InverseTransformDirection(currentLookPos - transform.position);
                float lookAngle = Mathf.Atan2(lookDelta.x, lookDelta.z) * Mathf.Rad2Deg;

                // are we beyond the threshold of where need to turn to face the camera?
                if (Mathf.Abs(lookAngle) > autoTurnThresholdAngle)
                {
                    turnAmount += lookAngle * autoTurnSpeed * .001f;
                }
            }

        }


        private void ApplyExtraTurnRotation()
        {
            // help the character turn faster (this is in addition to root rotation in the animation)
            float turnSpeed = Mathf.Lerp(advancedSettings.stationaryTurnSpeed, advancedSettings.movingTurnSpeed,
                                         forwardAmount);
            transform.Rotate(0, turnAmount * turnSpeed * Time.deltaTime, 0);
            
        }


        // Configura a fricção para baixo ou alto, dependendo se estamos nos movendo
        private void SetFriction()
        {

            //if (isGround)
            //{

                // set friction to low or high, depending on if we're moving
                if (playerInput.magnitude == 0)
                {
                    // when not moving this helps prevent sliding on slopes:
                    GetComponent<Collider>().material = advancedSettings.highFrictionMaterial;
                }
                else
                {
                    // but when moving, we want no friction:
                    GetComponent<Collider>().material = advancedSettings.zeroFrictionMaterial;
                }
            //}
            //else
            //{
                // while in air, we want no friction against surfaces (walls, ceilings, etc)
              //  GetComponent<Collider>().material = advancedSettings.zeroFrictionMaterial;
            //}
        }

        private void Move()
        {
            Vector3 finalSpeed = playerInput.normalized * moveSpeedValue * Time.fixedDeltaTime;
            navMeshAgent.Move(finalSpeed);
        }

        private void OnAttributeMoveSpeedChange(float prevValue, float currentValue)
        {
            moveSpeedValue = currentValue;
        }

    }
}
