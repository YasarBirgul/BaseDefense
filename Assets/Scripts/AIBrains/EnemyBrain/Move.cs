using Abstract;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.EnemyBrain
{
    public class Move : IState
    {
        #region Self Variables

        #region Public Variables
        
        public float TimeStuck;
        
        #endregion

        #region Serialized Variables,

        #endregion

        #region Private Variables

        private readonly EnemyAIBrain _enemyAIBrain;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        private static readonly int Speed = Animator.StringToHash("Speed");
        private Vector3 _lastPosition = Vector3.zero;

        #endregion
        
        #endregion
        public Move(NavMeshAgent navMeshAgent,Animator animator)
        {
            _navMeshAgent = navMeshAgent;
            _animator = animator;
        }
        public void UpdateIState()
        {
            var sqrDistance = (_enemyAIBrain.transform.position-_lastPosition).sqrMagnitude;
            if (sqrDistance == 0f) 
                TimeStuck += Time.deltaTime;
            _lastPosition = _enemyAIBrain.transform.position;
        }
        public void OnEnter()
        {
            _navMeshAgent.enabled = true;
            _navMeshAgent.SetDestination(_enemyAIBrain.Target.transform.position);
            _animator.SetFloat(Speed,1f);
        }
        public void OnExit()
        {
            //_navMeshAgent.enabled = true;
            //_animator.SetFloat(,1f);
        }
    }
}