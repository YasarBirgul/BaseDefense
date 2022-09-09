using Abstract;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.EnemyBrain
{
    public class MoveToBomb : IState
    {
        #region Self Variables

        #region Public Variables

        public bool BombIsAlive;
        
        #endregion

        #region Serialized Variables,

        #endregion

        #region Private Variables

        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        
        #endregion
        
        #endregion
        public MoveToBomb(NavMeshAgent navMeshAgent,Animator animator)
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