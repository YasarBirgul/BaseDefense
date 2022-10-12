using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.EnemyBrain.States
{
    public class Attack : IState
    {
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        private readonly EnemyAIBrain _enemyAIBrain;
        private static readonly int _attack = Animator.StringToHash("Attack");
        private static readonly int _run = Animator.StringToHash("Run");
        private float _attackTimer = 1f;
        private const float _refreshValue = 1f;

        public Attack(NavMeshAgent agent,Animator animator)
        {
            _navMeshAgent = agent;
            _animator = animator;
        }
        public void Tick()
        {
            _attackTimer -= Time.deltaTime;
            if (!(_attackTimer <= 0)) return;
            _animator.SetTrigger(_attack);
            _attackTimer = _refreshValue;
        }
        public void OnEnter()
        {
        }

        public void OnExit()
        {
            _animator.SetTrigger(_run);
        }
    }
}