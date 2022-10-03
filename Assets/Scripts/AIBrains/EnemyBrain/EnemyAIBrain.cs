using System;
using Abstract;
using Controllers;
using Data.UnityObject;
using Data.ValueObject.AIData;
using Enums;
using Interfaces;
using Managers;
using Signals;
using StateBehaviour;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace AIBrains.EnemyBrain
{ 
    public class EnemyAIBrain : MonoBehaviour,IReleasePoolObject
    {
        #region Self Variables

        #region Public Variables

        public Transform PlayerTarget;

        public Transform _turretTarget;
        
        public Transform MineTarget;
        
        public NavMeshAgent NavMeshAgent;
        
        public int Health;
        
        public EnemyTypes EnemyType;
        #endregion

        #region Serialized Variables,

        [SerializeField] private EnemyTypes enemyType;
        [SerializeField] private EnemyPhysicsDetectionController physicsDetectionController;
        [SerializeField] private Animator animator;
        #endregion

        #region Private Variables

        private int _levelID;
        private EnemyTypeData _data;
        private EnemyAIData _AIData;
        private int _damage;
        private float _attackRange;
        private float _attackSpeed;
        private float _moveSpeed;
        private Color _enemyColor;
        private float _chaseSpeed;
        private Vector3 _scaleSize;
        private float _navMeshRadius;
        private float _navMeshHeight;
        private StateMachine _stateMachine;
        private Transform _spawnPoint;
        private Chase chase;
        #endregion
        
        #endregion
        private void Awake()
        {
            _levelID = 0;
            _AIData = GetEnemyAIData();
            _data = GetEnemyData();
            SetEnemyData();
            GetStatesReferences();
        }
        private EnemyAIData GetEnemyAIData() => Resources.Load<CD_Enemy>("Data/CD_Enemy").EnemyAIData;
        private EnemyTypeData GetEnemyData() => _AIData.EnemyList[(int)enemyType];
        private void SetEnemyData()
        {  
            Health = _data.Health;
            _damage = _data.Damage;
            _attackRange = _data.AttackRange;
            _attackSpeed = _data.AttackSpeed;
            _chaseSpeed = _data.ChaseSpeed;
            _moveSpeed = _data.MoveSpeed;
            _enemyColor = _data.BodyColor;
            _scaleSize = _data.ScaleSize;
            _navMeshRadius = _data.NavMeshRadius;
            _navMeshHeight = _data.NavMeshHeight;
            _spawnPoint = _AIData.SpawnPositionList[_levelID];
            _turretTarget = _AIData.SpawnPositionList[_levelID].GetChild(Random.Range(0, _AIData.SpawnPositionList[_levelID].childCount));
        }
        private void GetStatesReferences()
        {
            NavMeshAgent = GetComponent<NavMeshAgent>();

            var search = new Search(this, NavMeshAgent, _spawnPoint);
            var move = new Move(NavMeshAgent,animator,this,_moveSpeed); 
            var attack = new Attack(NavMeshAgent,animator,this);
            var death = new Death(NavMeshAgent,animator,this,enemyType);
            chase = new Chase(NavMeshAgent,animator,this,_chaseSpeed);
            var moveToBomb = new MoveToBomb(NavMeshAgent,animator,this,_attackRange,_chaseSpeed);
            _stateMachine = new StateMachine();
          
            At(search,move,HasTurretTarget());
            At(move,chase,HasTarget());  
            At(chase,attack,AttackThePlayer()); 
            At(attack,chase,()=>attack.InPlayerAttackRange()==false);
            At(chase,move,TargetNull());
            
            _stateMachine.AddAnyTransition(death,AmIdead());
            _stateMachine.AddAnyTransition(moveToBomb,()=> physicsDetectionController.IsBombInRange());
           
            _stateMachine.SetState(search);
            void At(IState to,IState from,Func<bool> condition) =>_stateMachine.AddTransition(to,from,condition);

            Func<bool> HasTurretTarget() => () => _turretTarget != null;
            Func<bool> HasTarget() => () => PlayerTarget != null;
            Func<bool> AttackThePlayer() => () => PlayerTarget != null && chase.InPlayerAttackRange();
            Func<bool> TargetNull() => () => PlayerTarget is null;
            Func<bool> AmIdead() => () => Health <= 0 ;
        }
        private void Update() =>  _stateMachine.UpdateIState();
        public void ReleaseObject(GameObject obj, PoolType poolName)
        {
            PoolSignals.Instance.onReleaseObjectFromPool?.Invoke(poolName,obj);
        }
    }
}