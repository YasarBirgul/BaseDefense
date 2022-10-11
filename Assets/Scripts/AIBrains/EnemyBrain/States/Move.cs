using Abstract;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.EnemyBrain.States
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
        private static readonly int Run = Animator.StringToHash("Run");

        #endregion
        
        #endregion
        public Move(NavMeshAgent navMeshAgent,Animator animator,EnemyAIBrain enemyAIBrain,float moveSpeed)
        {
            _navMeshAgent = navMeshAgent;
            _animator = animator;
            _enemyAIBrain = enemyAIBrain;
            _moveSpeed = moveSpeed;
        }
        public void Tick()
        {
            if (Vector3.Distance(_enemyAIBrain.transform.position, _lastPosition) <= 0f)
            {
                TimeStuck += Time.deltaTime;

                _lastPosition = _enemyAIBrain.transform.position;
                _animator.SetFloat(Speed,_navMeshAgent.velocity.magnitude);
            }
        }
        public void OnEnter()
        {
            _navMeshAgent.enabled = true;
            _navMeshAgent.SetDestination(_enemyAIBrain._turretTarget.position);
            _navMeshAgent.speed = 1.529528f;
            _animator.SetTrigger(Run);
        }
        public void OnExit()
        {
           
        }
    }
}