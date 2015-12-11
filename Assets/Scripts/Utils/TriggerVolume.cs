using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(Collider))]
public class TriggerVolume : MonoBehaviour
{
    public Action<TriggerVolume, Collider> TriggerEnterAction;
    public Action<TriggerVolume, Collider> TriggerExitAction;
    public Action<TriggerVolume, Collider> TriggerStayAction;

    // escolher qual layer vai colidir
    [SerializeField] private LayerMask collisionMask = -1;
    private Collider colliderObject;
    // todos os objectos que estao dentro do triggerVolume
    private List<Collider> containingCollider = new List<Collider>();


    public int ContainingCount
    {
        get { return containingCollider.Count; }
    }


    private void Awake()
    {
        Debug.Log("Iniciando Trigger Volume");
        colliderObject = GetComponent<Collider>();
        colliderObject.isTrigger = true;
    }

    private void OnTriggerEnter(Collider coll)
    {
        // se não tive no layerMask que esta sendo checado, vota rapido!!!!
        if (!IsInLayerMask(coll.gameObject))
            return;
     
        // este objeto ja esta na nossa lista, retorna tambem
        if (containingCollider.Contains(coll))
            return;
        
        // caso seja um collider novo, adiciona
        containingCollider.Add(coll);

        if (TriggerEnterAction != null)
            TriggerEnterAction(this, coll);
    }

    private void OnTriggerExit(Collider coll)
    {
        // se não tive no layerMask que esta sendo checado, vota rapido!!!!
        if (!IsInLayerMask(coll.gameObject))
            return;

        // se não possui o collier sai do metodo
        if (!containingCollider.Contains(coll))
            return;

        // caso exista remove
        containingCollider.Remove(coll);

        if (TriggerExitAction != null)
            TriggerExitAction(this, coll);
    }

    private void OnTriggerStay(Collider coll)
    {
        // se não tive no layerMask que esta sendo checado, vota rapido!!!!
        if (!IsInLayerMask(coll.gameObject))
            return;

        // se não possui o collier sai do metodo
        if (!containingCollider.Contains(coll))
            return;


        if (TriggerStayAction != null)
            TriggerStayAction(this, coll);
    }

    private bool IsInLayerMask(GameObject _targetGameObject)
    {
        Debug.Log(collisionMask.value);
        Debug.Log(_targetGameObject.layer);

        return ((collisionMask.value & (1 << _targetGameObject.layer)) > 0);
    }


}
