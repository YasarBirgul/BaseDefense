using System.Collections;
using System.Collections.Generic;
using Abstract;
using UnityEngine;
using UnityEngine.AI;
using Interfaces;

namespace StateMachines.AIBrain.Workers.MoneyStates
{
    public class DropMoneyOnGateState : IState
    {
        private readonly NavMeshAgent _navmeshAgent;
        private readonly Animator _animator;
        private readonly Transform _startPos;
        private static readonly int Speed = Animator.StringToHash("Speed");
        public DropMoneyOnGateState(NavMeshAgent navMeshAgent, Animator animator, ref Transform startPos)
        {
            _navmeshAgent = navMeshAgent;
            _animator = animator;
            _startPos = startPos;
        }
        public void OnEnter()
        {
            _navmeshAgent.SetDestination(_startPos.position);

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
