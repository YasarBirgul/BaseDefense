using Abstract;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.SoldierBrain
{
    public class Patrol : IState
    {
        private SoldierAIBrain _soldierAIBrain;
        private Vector3? _destination;
        private NavMeshAgent _navMeshAgent;
        private Quaternion _lookRotation;
        private Vector3 _direction;
        private float _turnSpeed = 30;
        private Vector3 lastPosition = Vector3.zero;
        private float _timeStack;
        public Patrol(SoldierAIBrain soldierAIBrain,NavMeshAgent navMeshAgent)
        {
            _soldierAIBrain = soldierAIBrain;
            _navMeshAgent = navMeshAgent;
        }
        public void Tick()
        {
            if (_destination.HasValue != true || Vector3.Distance(_soldierAIBrain.transform.position,
                _destination.Value) <= _navMeshAgent.stoppingDistance)
            {
                FindRandomDestination();
                if (_destination != null) _navMeshAgent.destination = _destination.Value;
            }
            if (Vector3.Distance(_soldierAIBrain.transform.position, lastPosition) <= 0)
            {
                _timeStack += Time.deltaTime;
                if (_timeStack > 1f)
                {
                    _soldierAIBrain.transform.rotation = Quaternion.Slerp(_soldierAIBrain.transform.rotation, _lookRotation,Time.deltaTime * _turnSpeed);
                    _timeStack = 0;
                }
                else
                {
                    FindRandomDestination();
                }
            }
            lastPosition = _soldierAIBrain.transform.position;
        }
        public void OnEnter()
        {
            _timeStack = 0;
        }
        
        public void OnExit()
        {
            
        }
        private void FindRandomDestination()
        {
            var randomPositionX = Random.Range(-10, 10);
            var randomPositionZ = Random.Range(-10, 10);
            Vector3 randomPositionVector = _soldierAIBrain.transform.position + _soldierAIBrain.transform.forward*4f + new Vector3(randomPositionX, 0, randomPositionZ);
            _destination = new Vector3(randomPositionVector.x,_soldierAIBrain.transform.position.y,randomPositionVector.z);
            _direction = Vector3.Normalize(_destination.Value - _soldierAIBrain.transform.position);
            _direction = new Vector3(_direction.x, 0, _direction.z);
            _lookRotation = Quaternion.LookRotation(_direction);
        }
    }
}