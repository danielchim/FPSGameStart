using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Gameplay.Attribute
{
    public class AttributePool : MonoBehaviour
    {
        private Dictionary<AttributeType, Attribute> attributes = new Dictionary<AttributeType, Attribute>();
        // para poder acesso os atributos de fora
        //public Dictionary<AttributeType, Attribute> Attributes { get { return attributes; } }

        private void Awake()
        {
            Attribute[] childAttributes = transform.GetComponentsInChildren<Attribute>();
            attributes = new Dictionary<AttributeType, Attribute>(childAttributes.Length);
            foreach (Attribute childAttribute in childAttributes)
            {
                // se ja tive o attribute achamos um duplicado
                if (attributes.ContainsKey(childAttribute.AttributeType))
                {
                    Debug.Log("Atributo duplicado: " + childAttribute.AttributeType, this);
                    continue;
                }

                attributes.Add(childAttribute.AttributeType, childAttribute);

            }
        }

        public Attribute GetAttribute(AttributeType targetAttribute)
        {
            if (!attributes.ContainsKey(targetAttribute))
            {
                Debug.Log("Atributo duplicado: " + targetAttribute, this);
                return null;
            }

            return attributes[targetAttribute];
        }
    }
}
