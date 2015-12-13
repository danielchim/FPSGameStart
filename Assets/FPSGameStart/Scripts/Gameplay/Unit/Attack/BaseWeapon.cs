using UnityEngine;
using System.Collections;
using System;
using UnityPooler;

namespace Gameplay.Unit.Attack
{
    public class BaseWeapon : MonoBehaviour
    {
        [SerializeField]
        protected LayerMask hitLayerMask;
        [SerializeField]
        protected WeaponDefinition currentWeaponDefinition = new WeaponDefinition(30, 0.3f, 20, 10);
        [SerializeField]
        protected BaseBullet baseBullet;
        [SerializeField]
        protected int maxBulletPreload = 10;
        [SerializeField]
        protected Transform bulletExitPoint;

        private float lastShootTime = float.MinValue;

        protected virtual void Awake()
        {
            baseBullet.gameObject.PopulatePool(maxBulletPreload);
        }


        public virtual bool HaveAmmo()
        {
            return (currentWeaponDefinition.GetAmmo() > 0);
        }

        public virtual bool IsCoolDown()
        {
            return !(lastShootTime < Time.time);
        }


        public virtual void Shoot()
        {
            lastShootTime = Time.time + currentWeaponDefinition.GetCoolDown();
            currentWeaponDefinition.SpentAmmo();// remove uma 1

            BaseBullet bulletClone = baseBullet.gameObject.Get().GetComponent<BaseBullet>();
            bulletClone.transform.SetParent(transform);
            bulletClone.transform.localPosition = Vector3.zero;
            bulletClone.transform.forward = transform.forward;
            bulletClone.Initialize(this);
        }

        public WeaponDefinition GetWeaponDefinition()
        {
            return currentWeaponDefinition;
        }

        public LayerMask GetWeaponHitLayerMask()
        {
            return hitLayerMask;
        }

        public Transform GetBulletExitPoint()
        {
            return bulletExitPoint;
        }

        public void AddAmmo(int ammoAmount)
        {
            currentWeaponDefinition.AddAmmo(ammoAmount);
        }
    }
}
