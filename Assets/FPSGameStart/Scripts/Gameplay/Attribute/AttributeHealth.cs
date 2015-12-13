using UnityEngine;
using System.Collections;
using Gameplay.Attribute;
  
namespace Gameplay.Attribute
{
    public class AttributeHealth : Attribute
    {
        public override void ChangeValue(float targetValue)
        {
            Attribute armor = attributePool.GetAttribute(AttributeType.Armor);
            if (armor != null)
                targetValue += armor.CurrentValue;
            base.ChangeValue(targetValue);
        }
    }
}
