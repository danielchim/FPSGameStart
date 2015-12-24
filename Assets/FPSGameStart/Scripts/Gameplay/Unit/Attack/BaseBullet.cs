using UnityEngine;
using System.Collections;
using System;

namespace Gameplay.Unit.Attack
{
    public class BaseBullet : MonoBehaviour
    {
        protected WeaponDefinition weaponDefinition;
        protected BaseWeapon baseWeapon;
        protected LayerMask _layerMask;

        // caso pode ser sobre escrito
        public virtual void Initialize(BaseWeapon _baseWeapon)
        {
            weaponDefinition = _baseWeapon.GetWeaponDefinition();
            baseWeapon = _baseWeapon;
            _layerMask = _baseWeapon.GetWeaponHitLayerMask();
        }

        protected virtual void DestroyBullet(float seconds)
        {
            this.StartCoroutine(DestroyBulletAfterSecondsCoroutine(seconds));
        }

        private IEnumerator DestroyBulletAfterSecondsCoroutine(float seconds)
        {
            yield return new WaitForSeconds(seconds);

            SimplePool.Despawn(gameObject);
        }

        public void ApplyEffect(IHitByBullet[] hitByBullet)
        {
            foreach (IHitByBullet item in hitByBullet)
            {
                item.Hit(baseWeapon);
            }
        }
    }
}
