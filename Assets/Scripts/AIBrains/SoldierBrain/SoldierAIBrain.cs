﻿using System;
using System.Collections.Generic;
using Abstract;
using Controllers.Bullet;
using Controllers.Soldier;
using Data.UnityObject;
using Data.ValueObject.AIData;
using Data.ValueObject.WeaponData;
using Managers;
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
        
        public bool HasReachedSlotTarget;
        public bool HasReachedFrontYard;
        public bool HasEnemyTarget = false;
        
        public Transform TentPosition; 
        public Transform FrontYardStartPosition;
        public List<IDamagable> enemyList = new List<IDamagable>();
        public Transform EnemyTarget;
        #endregion

        #region Serialized Variables

        [SerializeField] private SoldierPhysicsController physicsController;
        [SerializeField] private Animator animator;
        [SerializeField] private Transform weaponHolder;
        #endregion

        #region Private Variables
        
        private NavMeshAgent _navMeshAgent;
       
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
        private Vector3 _slotTransform;
        private bool HasSoldiersActivated;
        private List<WeaponData> _weaponDatas;
        // private bool dead { get; set; }
        
        #endregion
        #endregion
        private void Awake()
        {
            _data = GetSoldierAIData();
            _weaponDatas = WeaponData();
            SetSoldierAIData();
            InitPool();
        } private void Start()
        {
            GetStateReferences();
        }
        private SoldierAIData GetSoldierAIData() => Resources.Load<CD_AI>("Data/CD_AI").SoldierAIData;
        private List<WeaponData> WeaponData() => Resources.Load<CD_Weapon>("Data/CD_Weapon").WeaponData;
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
        private void InitPool()
        {
            ObjectPoolManager.Instance.AddObjectPool(BulletFactoryMethod,TurnOnBullet,TurnOffBullet,"Bullet",15,false);
        }
        private GameObject BulletFactoryMethod()
        {
            return Instantiate(_weaponDatas[0].Bullet,weaponHolder.position,Quaternion.identity,weaponHolder.transform);
        }
        private void TurnOnBullet(GameObject bulletPrefab)
        {
            bulletPrefab.SetActive(true);
        }
        private void TurnOffBullet(GameObject bulletPrefab)
        {
            bulletPrefab.SetActive(false);
        } 
        public void GetObjectFromPool()
        {
            var BulletPrefab = ObjectPoolManager.Instance.GetObject<GameObject>("Bullet");
            Debug.Log(" BulletPrefab : " + BulletPrefab);
            BulletPrefab.GetComponent<BulletPhysicsController>().soldierAIBrain = this;
            Debug.Log(" Get :" + BulletPrefab.GetComponent<BulletPhysicsController>().soldierAIBrain);
            FireBullet(BulletPrefab);
        }
        public void FireBullet(GameObject BulletPrefab)
        {
            BulletPrefab.transform.rotation = _navMeshAgent.transform.rotation;
            var AgentTransform = _navMeshAgent.transform;
            // bullet.AddRelativeForce(Vector3.forward*10);
           var rigidBodyBullet = BulletPrefab.GetComponent<Rigidbody>();
           rigidBodyBullet.AddForce(AgentTransform.forward*70,ForceMode.VelocityChange);
           
           
        }
        private void GetStateReferences()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            var idle = new Idle(this,TentPosition,_navMeshAgent,animator);
            var moveToSlotZone = new MoveToSlotZone(this,_navMeshAgent,HasReachedSlotTarget,_slotTransform,animator);
            var wait = new Wait(animator,_navMeshAgent);
            var moveToFrontYard = new MoveToFrontYard(this,_navMeshAgent,FrontYardStartPosition,animator);
            var patrol = new Patrol(this,_navMeshAgent,animator);
            var attack = new Attack(this,_navMeshAgent,animator);
            var soldierDeath = new SoldierDeath();
            var reachToTarget = new DetectTarget();
            var shootTarget = new ShootTarget();
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
        public void SetEnemyTargetTransform()
        {
            EnemyTarget = enemyList[0].GetTransform(); 
            HasEnemyTarget = true;
        }

        public void EnemyTargetStatus()
        {
            if (enemyList.Count != 0)
            {
                EnemyTarget = enemyList[0].GetTransform();
            }
            else
            {
                HasEnemyTarget = false;
            }
        }
        public void RemoveTarget()
        {
            enemyList.RemoveAt(0);
            enemyList.TrimExcess();
            EnemyTarget = null;
            EnemyTargetStatus();
        }
    }
}