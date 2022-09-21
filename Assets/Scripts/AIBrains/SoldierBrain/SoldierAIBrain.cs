using System;
using System.Collections.Generic;
using Abstract;
using Data.UnityObject;
using Data.ValueObject.AIData;
using Signals;
using Sirenix.OdinInspector;
using StateBehaviour;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.SoldierBrain
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class SoldierAIBrain : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        public bool HasReachedTarget;
        public bool HasReachedFrontYard;
        
        public Transform TentPosition; 
        public Transform FrontYardStartPosition;
        
        #endregion

        #region Serialized Variables
        
        #endregion

        #region Private Variables

        [ShowInInspector] private List<IDamagable> _damagablesList;
        
        private NavMeshAgent _navMeshAgent;
       
        private Animator _animator;

        [Header("Data")]
        private SoldierAIData _data;
        private int _damage;
        private float _soldierSpeed;
        private float _attackRadius;
        private Coroutine _attackCoroutine;
        private float _attackDelay;
        private int _health;
        private Transform _spawnPoint;
        private StateMachine _stateMachine;
        private Vector3 _slotTransform;
        private bool HasSoldiersActivated;
        
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
            var moveToSlotZone = new MoveToSlotZone(this,_navMeshAgent,HasReachedTarget,_slotTransform);
            var wait = new Wait();
            var moveToFrontYard = new MoveToFrontYard(this,_navMeshAgent,FrontYardStartPosition);
            var patrol = new Patrol();
            var reachToTarget = new DetectTarget();
            var shootTarget = new ShootTarget();
            _stateMachine = new StateMachine();
            
            At(idle,moveToSlotZone,hasSlotTransformList());
            At(moveToSlotZone,moveToFrontYard,hasSoldiersActivated());
            At(moveToSlotZone, wait, hasReachToSlot());
            At(wait,moveToFrontYard,hasSoldiersActivated());
            At(moveToFrontYard, patrol, hasReachedFrontYard());

            _stateMachine.SetState(idle);
            void At(IState to,IState from,Func<bool> condition) =>_stateMachine.AddTransition(to,from,condition);

            Func<bool> hasSlotTransformList() => () => _slotTransform!= null;
            Func<bool> hasReachToSlot() => () => _slotTransform != null && HasReachedTarget;
            Func<bool> hasSoldiersActivated() => () => FrontYardStartPosition != null && HasSoldiersActivated;
            Func<bool> hasReachedFrontYard() => () => FrontYardStartPosition != null && HasReachedFrontYard;

        }
        private void Update() =>  _stateMachine.UpdateIState();

        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            AISignals.Instance.onSoldierActivation += OnSoldierActivation;
        }
        private void UnsubscribeEvents()
        {
            AISignals.Instance.onSoldierActivation -= OnSoldierActivation;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        public void GetSlotTransform(Vector3 slotTransfrom)
        {
            _slotTransform = slotTransfrom;
        }
        private void OnSoldierActivation()
        {
            HasSoldiersActivated = true;
        }
    }
}