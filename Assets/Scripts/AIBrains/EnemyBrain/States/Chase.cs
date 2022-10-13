using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.EnemyBrain.States
{
    public class Chase : IState
    {
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        private readonly EnemyAIBrain _enemyAIBrain;
        private static readonly int _speed = Animator.StringToHash("Speed");
        private static readonly int _run = Animator.StringToHash("Run");
        private const float _runSpeed = 6.256048f;
        public Chase(EnemyAIBrain enemyAIBrain,NavMeshAgent agent,Animator animator)
        {
            _enemyAIBrain = enemyAIBrain;
            _navMeshAgent = agent;
            _animator = animator;
        }
        public void Tick()
        {
            _navMeshAgent.destination = _enemyAIBrain.CurrentTarget.position;
            _animator.SetFloat(_speed,_navMeshAgent.velocity.magnitude);
        }
        public void OnEnter()
        {

            _navMeshAgent.SetDestination(_enemyAIBrain.CurrentTarget.position);
            _animator.SetTrigger(_run);
            _navMeshAgent.speed = _runSpeed;
        }
        public void OnExit()
        {
        }
    }
}