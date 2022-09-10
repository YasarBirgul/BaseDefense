using System;
using System.Collections.Generic;
using Abstract;
using Data.UnityObject;
using Data.ValueObject.AIData;
using Enums;
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
        
        public NavMeshAgent NavMeshAgent;

        // public List<Transform> TurretList;
        
        #endregion

        #region Serialized Variables,

        [SerializeField] private EnemyTypes enemyType;
        
        #endregion

        #region Private Variables

        private EnemyTypeData _data;
        private int _health;
        private int _damage;
        private float _attackRange;
        private float _moveSpeed;
        private float _chaseSpeed;
        private Animator _animator;
        private StateMachine _stateMachine;

        #endregion
        
        #endregion
        private void Awake()
        {
            _data = GetData();
            SetEnemyData();
            GetStatesReferences();
        }
        private EnemyTypeData GetData() => Resources.Load<CD_AI>("Data/CD_AI").EnemyAIData.EnemyList[(int)enemyType];
        private void SetEnemyData()
        {
            _health = _data.Health;
            _damage = _data.Damage;
            _attackRange = _data.AttackRange;
            _chaseSpeed = _data.ChaseSpeed;
            _moveSpeed = _data.MoveSpeed;
        }
        private void GetStatesReferences()
        {
            NavMeshAgent = GetComponent<NavMeshAgent>(); 
            _animator = GetComponent<Animator>();
            var move = new Move(NavMeshAgent,_animator,this,_moveSpeed); 
            var attack = new Attack(NavMeshAgent,_animator,this,_attackRange);
            var death = new Death(NavMeshAgent,_animator);
            var chase = new Chase(NavMeshAgent,_animator,this,_attackRange,_chaseSpeed);
            var moveToBomb = new MoveToBomb(NavMeshAgent,_animator);
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
            Func<bool> AttackRange() => () => Target != null && chase.InPlayerAttackRange();
            Func<bool> AttackOffRange() => () => Target != null && !attack.IsPlayerAttackRange();
            Func<bool> TargetNull() => () => Target is null;
        }
        private void Update() => _stateMachine.UpdateIState();
    }
}