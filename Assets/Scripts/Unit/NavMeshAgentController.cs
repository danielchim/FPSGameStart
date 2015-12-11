using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshAgentController : MonoBehaviour
{
    // vamos ouvir essa action
    public Action OnReachDestination;


    private NavMeshAgent navMeshAgent;
    private int areaMask;
    private bool reachDestination;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        // nossa variavel recebe a area do nosso NavMeshAgent
        areaMask = navMeshAgent.areaMask;
    }

    // seta a destino
    public void SetDestination(Vector3 _targetDestination)
    {
        // vamos verifica se o personagem tem um destino para ir
        NavMeshHit navMeshHit;
        NavMesh.SamplePosition(_targetDestination, out navMeshHit, 2.0f, areaMask);

        // se não achou o caminho retorna
        if (!navMeshHit.hit)
            return;

        reachDestination = false;
        // se achou
        navMeshAgent.SetDestination(navMeshHit.position);

        StopCoroutine(CheckDestination());
        StartCoroutine(CheckDestination());
    }

    internal void WarpPosition(Vector3 _initioalPosition)
    {
        NavMeshHit _navMeshHit;
        NavMesh.SamplePosition(_initioalPosition, out _navMeshHit, 2.0f, areaMask);

        if (!_navMeshHit.hit)
            return;

        navMeshAgent.Warp(_navMeshHit.position);
    }

    private IEnumerator CheckDestination()
    {
        while (!reachDestination)
        {
            if (!navMeshAgent.pathPending)
            {
                // se a distancia for menor a distancia de para, ele ainda não chegou
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                {
                    // ele não tem mais o caminho
                    if(!navMeshAgent.hasPath || Mathf.Abs(navMeshAgent.velocity.sqrMagnitude) < float.Epsilon)
                    {
                        //chegou ao destino
                        reachDestination = true;

                        if (OnReachDestination != null)
                            OnReachDestination();
                    }
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
