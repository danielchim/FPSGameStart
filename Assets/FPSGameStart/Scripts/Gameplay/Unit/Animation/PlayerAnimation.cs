using UnityEngine;
using System.Collections;

namespace Gameplay.Unit.Animation
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerAnimation : MonoSingleton<PlayerAnimation>
    {
        [System.NonSerialized]
        public float meshMoveSpeed = 4.0f;

        [System.NonSerialized]
        public float animSpeed = 1.5f;              // a public setting for overall animator animation speed

        private static Animator anim;                          // a reference to the animator on the character
        private AnimatorStateInfo currentBaseState;         // a reference to the current state of the animator, used for base layer
        private AnimatorStateInfo layer2CurrentState;   // a reference to the current state of the animator, used for layer 2

        static int reloadState = Animator.StringToHash("Layer2.Standing_Reload_DH");                // and are used to check state for various actions to occur

        //static int switchWeaponState = Animator.StringToHash("Layer2.WeaponSwap");
        public float movementSpeed = 10;
        public float turningSpeed = 60;


        public static float horizontalGetAxis, verticalAxis;

        void Start()
        {
            // initialising reference variables
            anim = GetComponent<Animator>();
            if (anim.layerCount == 2)
                anim.SetLayerWeight(1, 1);
        }

        void FixedUpdate()
        {
            float h = horizontalGetAxis;              // setup h variable as our horizontal input axis
            float v = verticalAxis;                // setup v variables as our vertical input axis
            anim.SetFloat("Speed", v);                          // set our animator's float parameter 'Speed' equal to the vertical input axis				
            anim.SetFloat("Direction", h);                      // set our animator's float parameter 'Direction' equal to the horizontal input axis		
            anim.speed = animSpeed;                             // set the speed of our animator to the public variable 'animSpeed'
                                                                //anim.SetLookAtWeight(lookWeight);					// set the Look At Weight - amount to use look at IK vs using the head's animation
            currentBaseState = anim.GetCurrentAnimatorStateInfo(0); // set our currentState variable to the current state of the Base Layer (0) of animation




            //Controls the movement speed
            if (v <= 0.0f)
            {
                meshMoveSpeed = 4;
            }
            else
            {
                meshMoveSpeed = 6;
            }

            if (anim.layerCount == 2)
            {
                layer2CurrentState = anim.GetCurrentAnimatorStateInfo(1);   // set our layer2CurrentState variable to the current state of the second Layer (1) of animation
            }
            //Reload weapon state
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetBool("Reloading", true);
            }
            else
            {
                anim.SetBool("Reloading", false);
            }
            //Switch weapon state
            /* if (layer2CurrentState.nameHash != reloadState || currentBaseState.nameHash != switchWeaponState)
             {
                 if (Input.GetButtonUp("Fire2"))
                 {
                     anim.SetBool("SwitchWeapon", true);
                 }
             }
             if (layer2CurrentState.nameHash == switchWeaponState)
             {
                 anim.SetBool("SwitchWeapon", false);
             }
             */



            CrouchIdleAnimation();
        }



        public static void ShootHipAnim(bool animValor)
        {
            anim.SetBool("ShootHip", animValor);
        }

        private void CrouchIdleAnimation()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                anim.SetBool("CrouchIdle", true);
            } else if (Input.GetKeyUp(KeyCode.C))
            {
                anim.SetBool("CrouchIdle", false);
            }
        }
    }
}
