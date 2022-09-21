using Abstract;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.SoldierBrain
{
    public class MoveToFrontYard: IState
    {
        private NavMeshAgent _navMeshAgent;
        private Transform _frontYardSoldierPosition;
        private float _stoppingDistance;
        private SoldierAIBrain _soldierAIBrain;
        public MoveToFrontYard(SoldierAIBrain soldierAIBrain,NavMeshAgent navMeshAgent,Transform frontYardSoldierPosition)
        {
            _navMeshAgent = navMeshAgent;
            _frontYardSoldierPosition = frontYardSoldierPosition;
            _stoppingDistance = navMeshAgent.stoppingDistance;
            _soldierAIBrain = soldierAIBrain;
        }
        public void Tick()
        {
            if ((_navMeshAgent.transform.position - _frontYardSoldierPosition.position).sqrMagnitude < _stoppingDistance)
            {
                _soldierAIBrain.HasReachedFrontYard = true;
            }
        } 
        public void OnEnter()
        {
            _navMeshAgent.enabled = true;
            _navMeshAgent.SetDestination(_frontYardSoldierPosition.position);
        }
        public void OnExit()
        {
            
        }
    }
}