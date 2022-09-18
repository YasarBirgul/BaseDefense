using System;
using System.Collections.Generic;
using Abstract;
using Data.UnityObject;
using Data.ValueObject.AIData;
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
        
        public NavMeshAgent NavMeshAgent;

        public Vector3 SlotTransform;

        #endregion

        #region Serialized Variables
        
        #endregion

        #region Private Variables

        [ShowInInspector] private List<IDamagable> _damagablesList;
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
        #endregion
        #endregion
        private void Awake()
        {
            _data = GetSoldierAIData();
            SetSoldierAIData();
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
            NavMeshAgent = GetComponent<NavMeshAgent>();
            var idle = new Idle();
            var moveToSlotZone = new MoveToSlotZone();
            var moveToFrontYard = new MoveToFrontYard();
            var patrol = new Patrol();
            var reachToTarget = new DetectTarget();
            var shootTarget = new ShootTarget();
            _stateMachine = new StateMachine();
            
            At(idle,moveToSlotZone,hasSlotTransformList());
            At(moveToFrontYard, idle, hasReachToSlot());
            

            _stateMachine.SetState(idle);
            void At(IState to,IState from,Func<bool> condition) =>_stateMachine.AddTransition(to,from,condition);

            Func<bool> hasSlotTransformList() => () => SlotTransform!= null;
            Func<bool> hasReachToSlot() => () => SlotTransform != null;
        }

        public void GetSlotTransform(Vector3 slotTransfrom)
        {
            SlotTransform = slotTransfrom;
        }
        private void Update() =>  _stateMachine.UpdateIState();
    }
}