using Abstract;
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
        
        #endregion
        
        #endregion
        public Chase(NavMeshAgent navMeshAgent,Animator animator)
        {
            
        }
        public void UpdateIState()
        {
            
        }
        public void OnEnter()
        {
            
        }

        public void OnExit()
        {
            
        }
    }
}