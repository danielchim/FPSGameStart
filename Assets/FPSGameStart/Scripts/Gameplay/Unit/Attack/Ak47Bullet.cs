using UnityEngine;
using System.Collections;

namespace Gameplay.Unit.Attack
{
    public class Ak47Bullet : BaseBullet
    {

        private Rigidbody _rigidbody;

        public float speed;


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }






        public override void Initialize(BaseWeapon _baseWeapon)
        {
            base.Initialize(_baseWeapon);


            _rigidbody.velocity = transform.forward * speed * Time.deltaTime;

            Ray shootRay = new Ray(transform.position, transform.forward);
            Vector3 finalShoot = transform.position + transform.forward * weaponDefinition.GetRange();

            RaycastHit hit;

            shootRay.origin = transform.position;
            shootRay.direction = transform.forward;

            if (Physics.Raycast(shootRay, out hit ,weaponDefinition.GetRange(), _layerMask.value))
            {
                finalShoot = hit.point;
                IHitByBullet[] hitBulet = hit.transform.GetComponents<IHitByBullet>();
                base.ApplyEffect(hitBulet);
            }

            DestroyBullet(.3f);
        }
    }
}
