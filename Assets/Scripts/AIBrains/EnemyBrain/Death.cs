using Abstract;
using Managers;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.EnemyBrain
{ 
    public class Death : IState
    {
        #region Self Variables

        #region Public Variables
        
        public bool IsDead;
        
        #endregion

        #region Serialized Variables,
        
        #endregion

        #region Private Variables

        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        private EnemyAIBrain _enemyAIBrain;
        #endregion
        
        #endregion
        public Death(NavMeshAgent navMeshAgent,Animator animator,EnemyAIBrain enemyAIBrain)
        {
            _navMeshAgent = navMeshAgent;
            _animator = animator;
            _enemyAIBrain = enemyAIBrain;
        }
        public void UpdateIState()
        {
            
        }
        public void OnEnter()
        {
            Debug.Log("OnEnter");
            ObjectPoolManager.Instance.ReturnObject(_enemyAIBrain.gameObject,_enemyAIBrain.EnemyType.ToString());
        }
        public void OnExit()
        {
            throw new System.NotImplementedException();
        }
    }
}