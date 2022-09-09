using System;
using Abstract;
using StateBehaviour;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.EnemyBrain
{
    public class EnemyAIBrain : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        public Transform Target;
        
        #endregion

        #region Serialized Variables,
        
        #endregion

        #region Private Variables

        private NavMeshAgent _navMeshAgent;
        private Animator _animator;
        private StateMachine _stateMachine;

        #endregion
        
        #endregion
        private void Awake()
        {
            GetStatesReferences();
        }
        private void GetStatesReferences()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>(); 
            _animator = GetComponent<Animator>();
            var move = new Move(_navMeshAgent,_animator); 
            var attack = new Attack(_navMeshAgent,_animator);
            var death = new Death(_navMeshAgent,_animator);
            var chase = new Chase(_navMeshAgent,_animator);
            var moveToBomb = new MoveToBomb(_navMeshAgent,_animator);
            _stateMachine = new StateMachine();
            At(move,chase,HasTarget());  
            At(chase,attack,AttackRange()); 
            At(attack,chase,AttackOffRange());
            At(chase,move,TargetNull());
            
            _stateMachine.AddAnyTransition(death,()=> death.IsDead);
            At(moveToBomb, attack, () => moveToBomb.BombIsAlive);
            _stateMachine.SetState(move);
            void At(IState to,IState from,Func<bool> condition) =>_stateMachine.AddTransition(to,from,condition);
            
            Func<bool> HasTarget() => () => Target != null;
            Func<bool> AttackRange() => () => Target != null && chase.IsPlayerInRange;
            Func<bool> AttackOffRange() => () => Target != null && !chase.IsPlayerInRange;
            Func<bool> TargetNull() => () => Target is null;
        }

        private void Update() => _stateMachine.UpdateIState();
    }
}