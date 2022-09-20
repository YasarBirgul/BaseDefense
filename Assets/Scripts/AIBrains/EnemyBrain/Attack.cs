using System;
using Abstract;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.EnemyBrain
{
    public class Attack : IState
    {
        private readonly EnemyAIBrain _enemyAIBrain;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        private readonly float _attackRange;

        private bool _inAttack;
        public bool InPlayerAttackRange() => _inAttack;
        public Attack(NavMeshAgent navMeshAgent, Animator animator, EnemyAIBrain enemyAIBrain)
        {
            _navMeshAgent = navMeshAgent;
            _animator = animator;
            _enemyAIBrain = enemyAIBrain;
            _attackRange = _navMeshAgent.stoppingDistance;
        }
        public void OnEnter()
        {
            if (_enemyAIBrain.PlayerTarget)
            {
                _inAttack = true;
                // _animator.SetTrigger("Attack");
            }
        } 
        public void OnExit()
        {
        }
        public void Tick()
        {
            if (_enemyAIBrain.PlayerTarget)
            {
                _navMeshAgent.destination =_enemyAIBrain.PlayerTarget.transform.position;
                CheckDistanceAttack();
            }
        }
        private void CheckDistanceAttack()
        {
            if (_navMeshAgent.remainingDistance > _attackRange)
                _inAttack = false;
        }
    }
}