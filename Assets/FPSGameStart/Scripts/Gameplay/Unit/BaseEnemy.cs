using UnityEngine;
using Gameplay.Unit.Movement;
using Gameplay.Unit;
using Gameplay.Utils;
using System;

namespace Gameplay.Unit
{
    [RequireComponent(typeof(NavMeshAgentController))]
    public class BaseEnemy : NewBaseUnit
    {
        private BehaviourState state = BehaviourState.Idle;
        private NavMeshAgentController navMeshAgentController;
        private TriggerVolume triggerVolume;
        private PlayerUnit currentTarget;

        protected override void Awake()
        {
            base.Awake();
            navMeshAgentController = GetComponent<NavMeshAgentController>();
            triggerVolume = GetComponentInChildren<TriggerVolume>();
        }

        protected override void Start()
        {
            base.Start();
            ChangeStateTo(BehaviourState.Patrolling);
        }

        // maquina de estado
        private void ChangeStateTo(BehaviourState _state)
        {
            if (state == BehaviourState.Idle && _state == BehaviourState.Patrolling)
            {
                SeekNewPosition();
            }
            state = _state;
        }

        // escuto a acao do navMeshAgentController
        private void OnEnable()
        {
            navMeshAgentController.OnReachDestination += OnReachDestination;
            triggerVolume.OnTriggerEnterEvent += OnTriggerEnterVolumeEvent;
            triggerVolume.OnTriggerExitEvent += OnTriggerExitVolumeEvent;
        }

        // deixo de escutar a acao do navMeshAgentController
        private void OnDisable()
        {
            navMeshAgentController.OnReachDestination -= OnReachDestination;
            triggerVolume.OnTriggerEnterEvent -= OnTriggerEnterVolumeEvent;
            triggerVolume.OnTriggerExitEvent -= OnTriggerExitVolumeEvent;

        }

        private void OnTriggerExitVolumeEvent(TriggerVolume volume, Collider collider)
        {
            currentTarget = null;
            ChangeStateTo(BehaviourState.Patrolling);
        }

        private void OnTriggerEnterVolumeEvent(TriggerVolume volume, Collider collider)
        {
            currentTarget = collider.GetComponent<PlayerUnit>();
            ChangeStateTo(BehaviourState.SeekingTarget);
        }


        private void SeekNewPosition()
        {
            Vector3 randomPosition = new Vector3(UnityEngine.Random.Range(-25.0f, 25.0f), 0, UnityEngine.Random.Range(-25.0f, 25.0f));
            navMeshAgentController.SetDestination(randomPosition);
        }

        private void OnReachDestination(Vector3 startPosition, Vector3 endPosition)
        {
            if (state == BehaviourState.Patrolling)
                SeekNewPosition();
        }

        private void Update()
        {
            if (state == BehaviourState.SeekingTarget)
            {
                navMeshAgentController.SetDestination(currentTarget.transform.position);
            }
        }
    }

}
