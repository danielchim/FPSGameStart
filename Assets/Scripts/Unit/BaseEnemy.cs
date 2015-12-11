using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(NavMeshAgentController))]
public class BaseEnemy : MonoBehaviour
{
    private NavMeshAgentController navMeshAgentController;

    private void Awake()
    {
        navMeshAgentController = GetComponent<NavMeshAgentController>();
    }

    private void Start()
    {
        SeekNewPosition();
    }

    // escuto a acao do navMeshAgentController
    private void OnEnable()
    {
        navMeshAgentController.OnReachDestination += SeekNewPosition;
    }

    // deixo de escutar a acao do navMeshAgentController
    private void OnDisable()
    {
        navMeshAgentController.OnReachDestination -= SeekNewPosition;
    }

    private void SeekNewPosition()
    {
        Vector3 randomPosition = new Vector3(UnityEngine.Random.Range(-25.0f, 25.0f), 0, UnityEngine.Random.Range(-25.0f, 25.0f));
        navMeshAgentController.SetDestination(randomPosition);
    }
}
