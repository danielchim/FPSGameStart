using UnityEngine;
using System.Collections;
using System;
using Gameplay.Unit;

namespace Gameplay
{
    [RequireComponent(typeof(TriggerVolume))]
    public abstract class BasePickAble : MonoBehaviour
    {
        private TriggerVolume _triggerVolume;

        private void Awake()
        {
            _triggerVolume = GetComponent<TriggerVolume>();
        }
        private void OnEnable()
        {
            _triggerVolume.TriggerEnterAction += OnTriggerPlayer;
        }

        private void OnDisable()
        {
            _triggerVolume.TriggerEnterAction -= OnTriggerPlayer;
        }

        private void OnTriggerPlayer(TriggerVolume triggerVolume, Collider coll)
        {
            PlayerUnit playerUnit = coll.gameObject.GetComponent<PlayerUnit>();
            OnPicked(playerUnit);
            Destroy(gameObject);
            Debug.Log("Player em Contato.");
        }

        protected abstract void OnPicked(PlayerUnit playerUnit);
    }
}
