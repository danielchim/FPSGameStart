using UnityEngine;
using System.Collections;
using System;

namespace Gameplay.Attribute
{
    public class Attribute : MonoBehaviour
    {
        //delegate
        public delegate void AttributeDelegate(float prevValue, float currentValue);
        public event AttributeDelegate OnAttributeChange;
        public event AttributeDelegate OnAttributeReset;
        public event AttributeDelegate OnAttributeOver;


        [SerializeField] private AttributeType attributeType;

        protected float maxValue = float.MaxValue;
        protected float currentValue;
        protected AttributePool attributePool;

        public float Percent { get { return currentValue / maxValue; } }
        public AttributeType AttributeType { get { return attributeType; } }
        public float CurrentValue { get { return currentValue; } }

        public void Initialize(float initialValue, float _maxValue)
        {
            attributePool = GetComponentInParent<AttributePool>();
            SetMaxValue(_maxValue);
            SetInitialValue(initialValue);
            DispatchResetEvent(0, initialValue);
        }

        // sera setado de foro
        public void SetMaxValue(float _maxValue)
        {
            float currentPercent = Percent;
            maxValue = _maxValue;
            SetInitialValue(currentValue*currentPercent);
        }

        // sera setado de foro
        public virtual void SetInitialValue(float initialValue)
        {
            float prevMaxValue = currentValue;
            currentValue = initialValue;
            currentValue = Mathf.Clamp(currentValue,0, maxValue);
            DispatchChangetEvent(prevMaxValue, currentValue);
        }

        // sera setado de foro
        // soma o currentvalue
        public virtual void ChangeValue(float targetValue)
        {
            float prevMaxValue = currentValue;
            currentValue += targetValue;
            currentValue = Mathf.Clamp(currentValue, 0, maxValue);
            DispatchChangetEvent(prevMaxValue, currentValue);

            if (currentValue <= 0)
                DispatchOvertEvent(prevMaxValue, currentValue);
        }

        private void DispatchResetEvent(float _prevValue, float _currentValue)
        {
            if (OnAttributeReset != null)
                OnAttributeReset(_prevValue, _currentValue);
        }

        private void DispatchChangetEvent(float _prevValue, float _currentValue)
        {
            this.name = attributeType + " - " + _currentValue + "/" + maxValue + " (" + Percent + ") ";
            if (OnAttributeChange != null)
                OnAttributeChange(_prevValue, _currentValue);
        }

        private void DispatchOvertEvent(float _prevValue, float _currentValue)
        {
            if (OnAttributeOver != null)
                OnAttributeOver(_prevValue, _currentValue);
        }
    }
}
