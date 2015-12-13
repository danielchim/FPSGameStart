using UnityEngine;
using Gameplay.Unit.Movement;
using Gameplay.Unit;

namespace Gameplay.Unit
{
    [RequireComponent(typeof(NavMeshAgentController))]
    public class BaseEnemy : NewBaseUnit
    {
        private NavMeshAgentController navMeshAgentController;

        protected override void Awake()
        {
            base.Awake();
            navMeshAgentController = GetComponent<NavMeshAgentController>();
        }

        protected override void Start()
        {
            base.Start();
            SeekNewPosition();
        }

        // escuto a acao do navMeshAgentController
        private void OnEnable()
        {
            navMeshAgentController.OnReachDestination += OnReachDestination;
        }

        // deixo de escutar a acao do navMeshAgentController
        private void OnDisable()
        {
            navMeshAgentController.OnReachDestination -= OnReachDestination;
        }

        private void SeekNewPosition()
        {
            Vector3 randomPosition = new Vector3(UnityEngine.Random.Range(-25.0f, 25.0f), 0, UnityEngine.Random.Range(-25.0f, 25.0f));
            navMeshAgentController.SetDestination(randomPosition);
        }

        private void OnReachDestination(Vector3 startPosition, Vector3 endPosition)
        {
            SeekNewPosition();
        }
    }

}
