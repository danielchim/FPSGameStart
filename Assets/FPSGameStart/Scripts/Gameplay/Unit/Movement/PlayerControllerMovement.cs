using UnityEngine;
using System.Collections;
using Gameplay.Unit.Movement;
using System;
using Gameplay.Attribute;

namespace Gameplay.Unit.Movement
{
    [RequireComponent(typeof(NewBaseUnit))]
    public class PlayerControllerMovement : BaseMovement
    {
        private Vector3 playerInput = Vector3.zero;

        [SerializeField] private LayerMask groundLayer;
        private Quaternion mouseRotation = Quaternion.identity;

        private void FixedUpdate()
        {
            CheckInput();
            Move();
            Turn();
        }



        private void CheckInput()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            playerInput = new Vector3(horizontal, 0, vertical);

            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(mouseRay, out hit, 100, groundLayer.value))
            {
                Vector3 diff = hit.point - transform.position;
                diff.y = 0;
                mouseRotation = Quaternion.LookRotation(diff);
            }
        }
        private void Turn()
        {
            rigidBodyPlayer.MoveRotation(mouseRotation);
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
