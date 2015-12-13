using UnityEngine;
using System.Collections;
using System;


namespace Gameplay.Unit.Movement
{
    public class NavMeshAgentController : BaseMovement
    {
        public delegate void PathResultDelegate(Vector3 startPosition, Vector3 endPosition);
        // vamos ouvir essa action
        public event PathResultDelegate OnReachDestination;
        public event PathResultDelegate OnFail;

        private int areaMask;
        private bool reachDestination;

        private Vector3 startPosition;
        private Vector3 destinationPosition;

        protected override void Awake()
        {
            base.Awake();
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
            startPosition = navMeshHit.position;
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
                        if (navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete)
                        {
                            // ele não tem mais o caminho
                            if(!navMeshAgent.hasPath || Mathf.Abs(navMeshAgent.velocity.sqrMagnitude) < float.Epsilon)
                            {
                                //chegou ao destino
                                reachDestination = true;
                                destinationPosition = transform.position;
                                DispatchPosition();
                            }
                        } else
                        {
                            reachDestination = true;
                            if (OnFail != null)
                            {
                                DispatchPositionFail();
                            }
                        }
                    }
                }
                yield return new WaitForSeconds(0.1f);
            }
        }

        private void DispatchPosition()
        {
            if (OnReachDestination != null)
                OnReachDestination(startPosition, destinationPosition);
        }

        private void DispatchPositionFail()
        {
            if (OnFail != null)
                OnFail(startPosition, transform.position);
        }
    }
}
