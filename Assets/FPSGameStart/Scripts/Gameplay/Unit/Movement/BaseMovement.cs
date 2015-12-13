using UnityEngine;
using System.Collections;
using Gameplay.Attribute;
using System;

namespace Gameplay.Unit.Movement
{
    [RequireComponent(typeof(Rigidbody), typeof(NavMeshAgent))]
    public class BaseMovement : MonoBehaviour
    {
        protected NavMeshAgent navMeshAgent;
        protected Rigidbody rigidBodyPlayer;
        protected AttributePool attributePool;
        protected NewBaseUnit baseUnit;
        protected float moveSpeedValue;

        private Attribute.Attribute moveSpeedAttribute;

        protected virtual void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            rigidBodyPlayer = GetComponent<Rigidbody>();
            //attributePool = GetComponent<AttributePool>();
            baseUnit = GetComponent<NewBaseUnit>();
        }

        private void OnDisable()
        {
            moveSpeedAttribute.OnAttributeChange -= MoveSpeedAttributeChange;
        }

        public virtual void Initialize()
        {
            moveSpeedAttribute = baseUnit.AttributePool.GetAttribute(AttributeType.MoveSpeed);
            moveSpeedAttribute.OnAttributeChange += MoveSpeedAttributeChange;
            MoveSpeedAttributeChange(0, moveSpeedAttribute.CurrentValue);//força o inicio para o valor da velocidade
        }

        private void MoveSpeedAttributeChange(float prevValue, float currentValue)
        {
            moveSpeedValue = currentValue;
        }
    }
}
