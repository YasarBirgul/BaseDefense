using System.Security.Cryptography;
using Abstract;
using Signals;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.EnemyBrain
{
    public class Chase: IState
    {
        #region Self Variables

        #region Public Variables

        public bool IsPlayerInRange;
        
        #endregion

        #region Serialized Variables,

        #endregion

        #region Private Variables

        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        private readonly EnemyAIBrain _enemyAIBrain;
        private readonly float _attackRange;
        private readonly float _chaseSpeed;

        private bool _inAttack = false;
        #endregion
        
        #endregion
        public Chase(NavMeshAgent navMeshAgent,Animator animator,EnemyAIBrain enemyAIBrain,float chaseSpeed)
        {
            _navMeshAgent = navMeshAgent;
            _animator = animator;
            _enemyAIBrain = enemyAIBrain;
            _attackRange = navMeshAgent.stoppingDistance;
            _chaseSpeed = chaseSpeed;
        }

        public bool InPlayerAttackRange() => _inAttack;
        
        public void Tick()
        {
            _navMeshAgent.destination = _enemyAIBrain.PlayerTarget.transform.position;
            CheckDistanceChase();
        }
        public void OnEnter()
        {
            _inAttack = false;
            _navMeshAgent.speed = _chaseSpeed;
            _navMeshAgent.SetDestination(_enemyAIBrain.PlayerTarget.transform.position);
        } 
        public void OnExit()
        {
            
        }
        private void CheckDistanceChase()
        {
            if (_navMeshAgent.remainingDistance <= _attackRange)
            {
                _inAttack = true;
            }
        }
        
    }
}