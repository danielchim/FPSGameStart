using UnityEngine;
using System.Collections;
namespace Gameplay.Unit.Attack
{
    public class AimController : MonoBehaviour
    {
        private BaseWeapon currentWeapon;

        public BaseWeapon CurrentWeapon
        {
            get { return currentWeapon; }
        }

        private void Awake()
        {
            currentWeapon = GetComponentInChildren<BaseWeapon>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!currentWeapon.HaveAmmo())
                    return;

                if (currentWeapon.IsCoolDown())
                    return;

                currentWeapon.Shoot();
            }
        }
    }
}
