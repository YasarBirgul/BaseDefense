using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.EnemyBrain.States
{
    public class Move : IState
    {
        private readonly EnemyAIBrain _enemyAIBrain;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        private Vector3 _lastPosition = Vector3.zero;

        private float _timeStuck;
        private static readonly int _speed = Animator.StringToHash("Speed");
        private static readonly int _run = Animator.StringToHash("Run");
        private const float _chaseSpeed=2.202521f;
        public Move(EnemyAIBrain enemyAIBrain,NavMeshAgent agent,Animator animator)
        {
            _enemyAIBrain = enemyAIBrain;
            _navMeshAgent = agent;
            _animator = animator;
        }
        public void Tick()
        {
            if (Vector3.Distance(_enemyAIBrain.transform.position, _lastPosition) <= 0f)
                _timeStuck += Time.deltaTime;
            _lastPosition = _enemyAIBrain.transform.position;
            _animator.SetFloat(_speed,_navMeshAgent.velocity.magnitude);
        }
        public void OnEnter()
        {
            _navMeshAgent.enabled = true;
            _navMeshAgent.SetDestination(_enemyAIBrain.TurretTarget.position);
            _animator.SetTrigger(_run);
            _navMeshAgent.speed = _chaseSpeed;
        }
        public void OnExit()
        {
            
        }
    }
}