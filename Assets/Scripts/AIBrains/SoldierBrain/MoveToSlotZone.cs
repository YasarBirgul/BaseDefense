using System.Net.Configuration;
using Abstract;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.SoldierBrain
{
    public class MoveToSlotZone : IState
    {
        private NavMeshAgent _navMeshAgent;
        private bool _hasReachToTarget;
        private Vector3 _soldierPosition;
        private Vector3 _slotPosition;
        private float _stoppingDistance;
        private SoldierAIBrain _soldierAIBrain;

        public MoveToSlotZone(SoldierAIBrain soldierAIBrain,NavMeshAgent navMeshAgent,bool hasReachToTarget,Vector3 slotPosition)
        {
            _soldierAIBrain = soldierAIBrain;
            _navMeshAgent = navMeshAgent;
            _hasReachToTarget = hasReachToTarget;
            _slotPosition = slotPosition;
            _stoppingDistance = navMeshAgent.stoppingDistance;
        } 
        public void Tick()
        {
            if ((_navMeshAgent.transform.position - _slotPosition).sqrMagnitude < _stoppingDistance)
            {
                _hasReachToTarget = true;
                _soldierAIBrain.HasReachedTarget = _hasReachToTarget;
            }
        } 
        public void OnEnter()
        {
            _navMeshAgent.SetDestination(_slotPosition);
        }
        public void OnExit()
        {
            _navMeshAgent.enabled = false;
        }
    }
}