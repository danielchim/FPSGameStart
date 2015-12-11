using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(NavMeshAgentController))]
public class BaseUnit : MonoBehaviour
{

    private NavMeshAgentController navMeshAgentController;

    private void Awake()
    {
        navMeshAgentController = GetComponent<NavMeshAgentController>();
    }

    private void Start()
    {
        Initiliaze();
    }

    private void Initiliaze()
    {
        Vector3 initioalPosition = new Vector3(UnityEngine.Random.Range(-25.0f, 25.0f), 0, UnityEngine.Random.Range(-25.0f, 25.0f));
        navMeshAgentController.WarpPosition(initioalPosition);
        OnReachDestination();
    }

    //ouvindo o metodo do navMeshAgentController 
    private void OnEnable()
    {
        navMeshAgentController.OnReachDestination += OnReachDestination;
    }

    // parando de ouvir
    private void OnDisable()
    {
        navMeshAgentController.OnReachDestination -= OnReachDestination;
    }

    private void OnReachDestination()
    {
        Vector3 randomPosition = new Vector3(UnityEngine.Random.Range(-25.0f,25.0f), 0, UnityEngine.Random.Range(-25.0f, 25.0f));

        navMeshAgentController.SetDestination(randomPosition);
    }
}
