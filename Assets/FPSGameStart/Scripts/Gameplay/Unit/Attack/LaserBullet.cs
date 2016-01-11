using UnityEngine;
using System.Collections;

namespace Gameplay.Unit.Attack
{
    public class LaserBullet : BaseBullet
    {
        private LineRenderer _lineRenderer;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        public override void Initialize(BaseWeapon _baseWeapon)
        {
            base.Initialize(_baseWeapon);

            Ray shootRay = new Ray(transform.position, transform.forward);
            Vector3 finalShootPosition = transform.position + transform.forward * weaponDefinition.GetRange();
            RaycastHit hit;

            shootRay.origin = transform.position;
            shootRay.direction = transform.right;

            if (Physics.Raycast(shootRay, out hit, weaponDefinition.GetRange(), _layerMask.value))
            {
                finalShootPosition = hit.point;

                IHitByBullet[] hitByBullet = hit.transform.GetComponents<IHitByBullet>();
                base.ApplyEffect(hitByBullet);
            }

            //finalShootPosition = new Vector3(finalShootPosition.x, transform.position.y, transform.position.z);
            _lineRenderer.SetPosition(0, _baseWeapon.GetBulletExitPoint().position);
            _lineRenderer.SetPosition(1, finalShootPosition);

            DestroyBullet(0.3f);
        }
    }
}
