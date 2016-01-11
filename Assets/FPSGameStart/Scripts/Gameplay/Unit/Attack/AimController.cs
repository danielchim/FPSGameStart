using UnityEngine;
using Gameplay.Unit.Animation;

namespace Gameplay.Unit.Attack
{
    public class AimController : MonoBehaviour
    {
        private BaseWeapon currentWeapon;

        public BaseWeapon CurrentWeapon
        {
            get { return currentWeapon; }
        }


        private Animator anim;
        private void Awake()
        {
            currentWeapon = GetComponentInChildren<BaseWeapon>();

            anim = GetComponent<Animator>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!currentWeapon.HaveAmmo())
                    return;

                if (currentWeapon.IsCoolDown())
                    return;

                PlayerAnimation.ShootHipAnim(true);
                currentWeapon.Shoot();
            }

            if (Input.GetMouseButtonUp(0))
            {
                PlayerAnimation.ShootHipAnim(false);
            }
        }
    }
}
