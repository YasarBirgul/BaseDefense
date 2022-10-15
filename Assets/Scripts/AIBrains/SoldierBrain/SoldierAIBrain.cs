using System;
using System.Collections.Generic;
using AIBrains.SoldierBrain.States;
using Data.UnityObject;
using Data.ValueObject.AIData;
using Interfaces;
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
        
        public bool HasReachedSlotTarget;
        public bool HasReachedFrontYard;
        public bool HasEnemyTarget = false;
        
        public Transform TentPosition;
        public Transform FrontYardStartPosition;
        public List<IDamageable> enemyList = new List<IDamageable>();
        public Transform EnemyTarget;
        public IDamageable DamageableEnemy;
        public Transform WeaponHolder;
        public bool HasSoldiersActivated;
        #endregion

        #region Serialized Variables
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private NavMeshAgent navMeshAgent;
        #endregion
        
        #region Private Variables

        [Header("Data")]
        private SoldierAIData _data;

        private StateMachine _stateMachine;
        private Vector3 _slotTransform;
        
        #endregion
        #endregion
        private void Awake()
        {
            _data = GetSoldierAIData();
        } private void Start()
        {
            GetStateReferences();
        }
        private SoldierAIData GetSoldierAIData() => Resources.Load<CD_AI>("Data/CD_AI").SoldierAIData;
        private void GetStateReferences()
        {
            var idle = new Idle(TentPosition,navMeshAgent);
            var moveToSlotZone = new MoveToSlotZone(this,navMeshAgent,HasReachedSlotTarget,_slotTransform,animator);
            var wait = new Wait(animator,navMeshAgent);
            var moveToFrontYard = new MoveToFrontYard(this,navMeshAgent,FrontYardStartPosition,animator);
            var patrol = new Patrol(this,navMeshAgent,animator);
            var attack = new Attack(this,navMeshAgent,animator);
            _stateMachine = new StateMachine();
            
            At(idle,moveToSlotZone,hasSlotTransformList());
            At(moveToSlotZone,moveToFrontYard,hasSoldiersActivated());
            At(moveToSlotZone, wait, hasReachToSlot());
            At(wait,moveToFrontYard,hasSoldiersActivated());
            At(moveToFrontYard, patrol, hasReachedFrontYard());
            At(patrol,attack,hasEnemyTarget());
            At(attack,patrol, hasNoEnemyTarget());

            _stateMachine.SetState(idle);
            void At(IState to,IState from,Func<bool> condition) =>_stateMachine.AddTransition(to,from,condition);

            Func<bool> hasSlotTransformList()=> ()=> _slotTransform!= null;
            Func<bool> hasReachToSlot()=> ()=> _slotTransform != null && HasReachedSlotTarget;
            Func<bool> hasSoldiersActivated()=> ()=> FrontYardStartPosition != null && HasSoldiersActivated;
            Func<bool> hasReachedFrontYard()=> ()=> FrontYardStartPosition != null && HasReachedFrontYard;
            Func<bool> hasEnemyTarget() => () => HasEnemyTarget;
            Func<bool> hasNoEnemyTarget() => () => !HasEnemyTarget;
        }
        private void Update() =>  _stateMachine.UpdateIState();
        public void GetSlotTransform(Vector3 slotTransfrom)=>  _slotTransform = slotTransfrom;
    }
}