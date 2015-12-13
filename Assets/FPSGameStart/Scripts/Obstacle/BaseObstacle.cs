using UnityEngine;
using System.Collections;
using System;

public class BaseObstacle : MonoBehaviour
{
    [SerializeField] private TriggerVolume triggerVolume;

    // fica ouvindo a acao | action
    private void OnEnable()
    {
        triggerVolume.TriggerEnterAction += _TriggerEnter;
        triggerVolume.TriggerExitAction += _TriggerExit;
    }

    // deixa de ouvir
    private void OnDisable()
    {
        triggerVolume.TriggerEnterAction -= _TriggerEnter;
        triggerVolume.TriggerExitAction -= _TriggerExit;
    }

    private void _TriggerExit(TriggerVolume _triggerVolume, Collider coll)
    {
    }

    private void _TriggerEnter(TriggerVolume _triggerVolume, Collider coll)
    {
    }
}
