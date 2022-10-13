using Abstract;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.WorkerBrain.MoneyWorker.States
{
    public class MoveToGateState : IState
    {
        private readonly NavMeshAgent _navmeshAgent;
        private readonly Animator _animator;
        private readonly Vector3 _waitPos;
        private readonly float _maxSpeed;
        private static readonly int Speed = Animator.StringToHash("Speed");

        public bool IsArrive = false;
        public MoveToGateState(NavMeshAgent navMeshAgent, Animator animator,Vector3 waitPos,float maxSpeed)
        {
            _navmeshAgent = navMeshAgent;
            _animator = animator;
            _waitPos = waitPos;
            _maxSpeed = maxSpeed;
        }
        public void OnEnter()
        {
            //isWalking anim
            _navmeshAgent.SetDestination(_waitPos);
            _navmeshAgent.speed = _maxSpeed;
        }

        public void OnExit()
        {
            IsArrive = false;
        }

        public void Tick()
        {
            _animator.SetFloat(Speed, _navmeshAgent.velocity.magnitude);
            if (_navmeshAgent.remainingDistance <= 0.1f)
            {
                IsArrive=true;
            }
        }
    } 
}
