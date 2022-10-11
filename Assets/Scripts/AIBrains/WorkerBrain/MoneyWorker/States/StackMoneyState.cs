using System;
using Abstract;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.WorkerBrain.MoneyWorker.States
{
    public class StackMoneyState : IState
    {
        private readonly NavMeshAgent _navmeshAgent;
        private readonly Animator _animator;
        private readonly MoneyWorkerAIBrain _moneyWorkerAIBrain;
        private bool isArrive;
        private static readonly int Speed = Animator.StringToHash("Speed");

        public Func<bool> IsArriveToMoney() => () => isArrive && _moneyWorkerAIBrain.IsAvailable();

        public StackMoneyState(NavMeshAgent navMeshAgent, Animator animator, MoneyWorkerAIBrain moneyWorkerAIBrain)
        {
            _navmeshAgent = navMeshAgent;
            _animator = animator;
            _moneyWorkerAIBrain = moneyWorkerAIBrain;
        }
        public void OnEnter()
        {
            _navmeshAgent.speed = 1.53f;
        }

        public void OnExit()
        {
            isArrive = false;
        }
        public void Tick()
        {
            if (_navmeshAgent.remainingDistance <= 0f)
            {
                _moneyWorkerAIBrain.CurrentTarget = null;
                isArrive = true;
            }
            _animator.SetFloat(Speed, _navmeshAgent.velocity.magnitude);
        }
    }
}
