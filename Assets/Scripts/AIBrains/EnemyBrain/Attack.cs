using Abstract;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.EnemyBrain
{
    public class Attack : IState
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables,

        #endregion

        #region Private Variables

        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        private readonly EnemyAIBrain _enemyAIBrain;
        private readonly float _attackRange;

        private bool _inAttack;
        
        #endregion
        
        #endregion

        public Attack(NavMeshAgent navMeshAgent,Animator animator,EnemyAIBrain enemyAIBrain,float attackRange)
        {
            _navMeshAgent = navMeshAgent;
            _animator = animator;
            _enemyAIBrain = enemyAIBrain;
            _attackRange = attackRange;

        }
        public bool IsPlayerAttackRange() => _inAttack; 
        public void UpdateIState()
        {
            _navMeshAgent.destination = _enemyAIBrain.Target.transform.position;
            CheckDistanceAttack();
        }

        public void OnEnter()
        {
            _inAttack = true;
            _navMeshAgent.SetDestination(_enemyAIBrain.Target.transform.position);
        }

        public void OnExit()
        {
            
        }
        private void CheckDistanceAttack()
        {
            if (_navMeshAgent.remainingDistance > _attackRange)
            {
                _inAttack = false;
            }
        }
    }
}