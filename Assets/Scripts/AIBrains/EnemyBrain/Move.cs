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

        #region Serialized Variables

        #endregion

        #region Private Variables

        private readonly EnemyAIBrain _enemyAIBrain;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        private static readonly int Speed = Animator.StringToHash("Speed");
        private Vector3 _lastPosition = Vector3.zero;
        private readonly float _moveSpeed;

        #endregion
        
        #endregion
        public Move(NavMeshAgent navMeshAgent,Animator animator,EnemyAIBrain enemyAIBrain,float moveSpeed)
        {
            _navMeshAgent = navMeshAgent;
            _animator = animator;
            _enemyAIBrain = enemyAIBrain;
            _moveSpeed = moveSpeed;
        }
        public void UpdateIState()
        {
           // var sqrDistance = (_enemyAIBrain.transform.position-_lastPosition).sqrMagnitude;
           // if (sqrDistance == 0f) 
           //     TimeStuck += Time.deltaTime;
           // _lastPosition = _enemyAIBrain.transform.position;
        }
        public void OnEnter()
        {
            _navMeshAgent.enabled = true;
            _navMeshAgent.speed = _moveSpeed;
            _navMeshAgent.SetDestination(_enemyAIBrain._turretTarget.position);
         //    _animator.SetFloat(Speed,1f);
        }
        public void OnExit()
        {
            //_navMeshAgent.enabled = true;
            //_animator.SetFloat(,1f);
        }
    }
}