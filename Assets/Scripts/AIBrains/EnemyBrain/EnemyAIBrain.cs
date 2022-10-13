using System;
using System.Collections.Generic;
using AIBrains.EnemyBrain.States;
using Data.UnityObject;
using Data.ValueObject.AIData;
using Enums;
using Interfaces;
using Managers;
using Signals;
using StateBehaviour;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.EnemyBrain
{ 
    public class EnemyAIBrain : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public bool IsBombSettled;
        public Transform CurrentTarget;
        public Transform TurretTarget;
        public int Health { get => _health; set => _health = value; } 

        #endregion
        
        #region Serialized Variables

        [SerializeField] 
        private Transform spawnPosition;
        
        [SerializeField]
        private EnemyTypes enemyType;

        #endregion

        #region Private Variables
        
        private int _health;
        private EnemyTypeData _data;
        private StateMachine _stateMachine;

        #endregion
        
        #endregion
        
        private void Awake()
        {   
            _data = GetEnemyAIData();
            Health = _data.Health;
        }
        private void OnEnable()
        {
            spawnPosition = AISignals.Instance.getSpawnTransform?.Invoke();
            
            CurrentTarget = AISignals.Instance.getRandomTransform?.Invoke();
            
            TurretTarget = CurrentTarget;

            Health = _data.Health;
        }
        private void Start()
        {   
            GetStatesReferences();
        }

        private EnemyTypeData GetEnemyAIData() => Resources.Load<CD_Enemy>("Data/CD_Enemy").EnemyAIData.EnemyList[(int)enemyType];
        
        private void GetStatesReferences()
        {
            
            var animator = GetComponentInChildren<Animator>();
            var navMeshAgent = GetComponent<NavMeshAgent>();
            
            var search = new Search(this,navMeshAgent,spawnPosition);
            var attack = new Attack(animator);
            var move = new Move(this,navMeshAgent,animator);
            var death = new Death(navMeshAgent,animator,this,enemyType);
            var chase = new Chase(this,navMeshAgent,animator);
            var moveToBomb = new MoveToBomb(navMeshAgent,animator);
            var baseAttack = new BaseAttack(navMeshAgent, animator);
            
            _stateMachine = new StateMachine();
            
            At(search,move,HasInitTarget());
            At(move,chase,HasTargetTurret()); 
            At(chase,attack,AttackRange()); 
            At(attack,chase,AttackOffRange()); 
            At(chase,move,TargetNull());
            At(move,baseAttack,IsEnemyReachedBase());
            At(baseAttack,chase,IsTargetChange());
            
            _stateMachine.AddAnyTransition(death,  IsDead());
            _stateMachine.AddAnyTransition(moveToBomb, ()=>IsBombSettled);
            
            _stateMachine.SetState(search);
            
            void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
            Func<bool> HasInitTarget() => () => TurretTarget != null;
            Func<bool> HasTargetTurret() => () => CurrentTarget != null && CurrentTarget.TryGetComponent(out PlayerManager player);
            Func<bool> AttackRange() => () => CurrentTarget != null  && (transform.position- CurrentTarget.transform.position).sqrMagnitude < Mathf.Pow(navMeshAgent.stoppingDistance,2);
            Func<bool> AttackOffRange() => () => CurrentTarget != null && (transform.position - CurrentTarget.transform.position).sqrMagnitude > Mathf.Pow(navMeshAgent.stoppingDistance,2);
            Func<bool> TargetNull() => () => CurrentTarget == null;
            Func<bool> IsDead() => () => Health <= 0;
            Func<bool> IsEnemyReachedBase() => () => CurrentTarget == TurretTarget && (transform.position - CurrentTarget.transform.position).sqrMagnitude < Mathf.Pow(navMeshAgent.stoppingDistance,2);
            Func<bool> IsTargetChange() => () => CurrentTarget != TurretTarget;
        }
        private void Update()
        {   
            _stateMachine.UpdateIState();
        }
        public void SetTarget(Transform target)
        {
            if (target == CurrentTarget)
            {
                return;
            }
            
            CurrentTarget = target;

            if (CurrentTarget != null) return;
            CurrentTarget = TurretTarget;
        }
    }
}
