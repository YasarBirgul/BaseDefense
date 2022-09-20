using System;
using System.Collections.Generic;
using Abstract;
using AIBrains.EnemyBrain;
using Data.UnityObject;
using Data.ValueObject.AIData;
using Sirenix.OdinInspector;
using StateBehaviour;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace AIBrains.SoldierBrain
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class SoldierAIBrain : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables
        
        #endregion

        #region Private Variables

        [ShowInInspector] private List<IDamagable> _damagablesList;
        
        private NavMeshAgent _navMeshAgent;
       
        private Animator _animator;

        [Header("Data")] 
        private SoldierAIData _data;

        public Transform TentPosition;
        private int _damage;
        private float _soldierSpeed;
        private float _attackRadius;
        private Coroutine _attackCoroutine;
        private float _attackDelay;
        private int _health;
        private Transform _spawnPoint;
        private StateMachine _stateMachine;
        private Vector3 _slotTransform;
        public bool hasReachedTarget;
        #endregion
        #endregion
        private void Awake()
        {
            _data = GetSoldierAIData();
            SetSoldierAIData();
        }
        private void Start()
        {
            GetStateReferences();
        }
        private SoldierAIData GetSoldierAIData() => Resources.Load<CD_AI>("Data/CD_AI").SoldierAIData;
        private void SetSoldierAIData()
        {
            _damage = _data.Damage;
            _soldierSpeed = _data.SoldierSpeed;
            _attackRadius = _data.AttackRadius;
            _attackCoroutine = _data.AttackCoroutine;
            _attackDelay = _data.AttackDelay;
            _health = _data.Health;
            _spawnPoint = _data.SpawnPoint;
        }
        private void GetStateReferences()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>(); 
            var idle = new Idle(this,TentPosition,_navMeshAgent);
            var moveToSlotZone = new MoveToSlotZone(this,_navMeshAgent,hasReachedTarget,_slotTransform);
            var wait = new Wait();
            var moveToFrontYard = new MoveToFrontYard();
            var patrol = new Patrol();
            var reachToTarget = new DetectTarget();
            var shootTarget = new ShootTarget();
            _stateMachine = new StateMachine();
            
            At(idle,moveToSlotZone,hasSlotTransformList());
            At(moveToSlotZone, wait, hasReachToSlot());
          
            _stateMachine.SetState(idle);
            void At(IState to,IState from,Func<bool> condition) =>_stateMachine.AddTransition(to,from,condition);

            Func<bool> hasSlotTransformList() => () => _slotTransform!= null;
            Func<bool> hasReachToSlot() => () => _slotTransform != null && hasReachedTarget;
        }
        private void Update() =>  _stateMachine.UpdateIState();
        public void GetSlotTransform(Vector3 slotTransfrom)
        {
            _slotTransform = slotTransfrom;
        }
    }
}