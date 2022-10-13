using Abstract;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.WorkerBrain.MoneyWorker.States
{
    public class DropMoneyOnGateState : IState
    {
        private readonly NavMeshAgent _navmeshAgent;
        private readonly Animator _animator;
        private readonly Vector3 _waitPos;
        private static readonly int Speed = Animator.StringToHash("Speed");
        public DropMoneyOnGateState(NavMeshAgent navMeshAgent, Animator animator, Vector3 waitPos)
        {
            _navmeshAgent = navMeshAgent;
            _animator = animator;
            _waitPos = waitPos;
        }
        public void OnEnter()
        {
            _navmeshAgent.SetDestination(_waitPos);

        }

        public void OnExit()
        {
            
        }
        public void Tick()
        {
            _animator.SetFloat(Speed, _navmeshAgent.velocity.magnitude);
        }
    }
}
