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
        
        #endregion
        
        #endregion

        public Attack(NavMeshAgent navMeshAgent,Animator animator)
        {
            
        }
        public void UpdateIState()
        {
            throw new System.NotImplementedException();
        }

        public void OnEnter()
        {
            throw new System.NotImplementedException();
        }

        public void OnExit()
        {
            throw new System.NotImplementedException();
        }
    }
}