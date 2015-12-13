using UnityEngine;
using System.Collections;
using Gameplay.Attribute;
using Gameplay.Unit.Movement;
using Gameplay.Unit.Attack;
using System;

namespace Gameplay.Unit
{
    [RequireComponent(typeof(BaseMovement))]
    public class NewBaseUnit : MonoBehaviour, IHitByBullet
    {
        private AttributePool attributePool;
        private BaseMovement baseMovement;

        public AttributePool AttributePool { get { return attributePool; } }


        protected virtual void Awake()
        {
            attributePool = GetComponentInChildren<AttributePool>();
            baseMovement = GetComponent<BaseMovement>();
        }

        protected virtual void Start()
        {
            attributePool.GetAttribute(AttributeType.MoveSpeed).Initialize(5,10);
            attributePool.GetAttribute(AttributeType.Health).Initialize(100, 100);
            baseMovement.Initialize();
        }

        public void Hit(BaseWeapon baseWeapon)
        {
            attributePool.GetAttribute(AttributeType.Health).ChangeValue(-baseWeapon.GetWeaponDefinition().GetDamage());
        }
    }
}
