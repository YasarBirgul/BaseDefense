using System;
using AIBrains.EnemyBrain.States;
using Controllers.Player;
using Controllers.Soldier;
using Data.UnityObject;
using Data.ValueObject.AIData;
using Enums;
using Interfaces;
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
        public int Health {get => _health; set => _health = value;}
        
        public PlayerPhysicsController PlayerPhysicsController;
        public SoldierHealthController SoldierHealthController;
        
        #endregion
        
        #region Serialized Variables

        [SerializeField] 
        private Transform spawnPosition;
        
        [SerializeField]
        private EnemyTypes enemyType;

        [SerializeField]
        private NavMeshAgent navMeshAgent;

        [SerializeField]
        private Animator animator;

        #endregion

        #region Private Variables
        
        private int _health;
        private EnemyTypeData _data;
        private StateMachine _stateMachine;
        private const int _enemyAttackPower=10;
        private Search _search;

        #endregion
        
        #endregion
        
        private void Awake()
        {   
            _data = GetEnemyAIData();
            _health = _data.Health;
            spawnPosition = AISignals.Instance.getSpawnTransform?.Invoke();
            CurrentTarget = AISignals.Instance.getRandomTransform?.Invoke();
            GetStatesReferences();
        }
        private void OnEnable()
        {
            GetComponentInChildren<IDamageable>().IsDead = false;
            TurretTarget = CurrentTarget;
            _stateMachine.SetState(_search);
            _health = _data.Health;
        }
        private EnemyTypeData GetEnemyAIData() => Resources.Load<CD_Enemy>("Data/CD_Enemy").EnemyAIData.EnemyList[(int)enemyType];
        private void GetStatesReferences()
        {
            _search = new Search(this,navMeshAgent,spawnPosition);
            var attack = new Attack(animator,this);
            var move = new Move(this,navMeshAgent,animator);
            var death = new Death(navMeshAgent,animator,this,enemyType);
            var chase = new Chase(this,navMeshAgent,animator);
            var moveToBomb = new MoveToBomb(navMeshAgent,animator);
            var baseAttack = new BaseAttack(navMeshAgent, animator);
            
            _stateMachine = new StateMachine();
            
            At(_search,move,HasInitTarget());
            At(move,chase,HasTargetTurret()); 
            At(chase,attack,AttackRange()); 
            At(attack,chase,AttackOffRange()); 
            At(chase,move,TargetNull());
            At(move,baseAttack,IsEnemyReachedBase());
            At(baseAttack,chase,IsTargetChange());
            
            _stateMachine.AddAnyTransition(death,  IsDead());
            _stateMachine.AddAnyTransition(moveToBomb, ()=>IsBombSettled);
            
            // _stateMachine.SetState(_search);
           
            void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
            
            Func<bool> HasInitTarget() => () => TurretTarget != null;
            Func<bool> HasTargetTurret() => () => CurrentTarget != null && (CurrentTarget.TryGetComponent(out PlayerPhysicsController playerPhysicsController) || CurrentTarget.TryGetComponent(out SoldierHealthController soldierHealthController));
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
            SoldierHealthController = null;
            PlayerPhysicsController = null;
        }
        public void CacheSoldier(SoldierHealthController soldierHealthController)
        {
            if (soldierHealthController == SoldierHealthController) return;
            SoldierHealthController = soldierHealthController;
        } 
        public void CachePlayer(PlayerPhysicsController playerPhysicsController)
        {
            PlayerPhysicsController = playerPhysicsController;
        }
        public void HitDamage()
        {
            if (SoldierHealthController != null)
            {
                int soldierHealth = SoldierHealthController.TakeDamage(_enemyAttackPower);
                if (soldierHealth <= 0)
                {
                    SoldierHealthController = null;
                    SetTarget(TurretTarget);
                }
            }
            if(PlayerPhysicsController != null)
            {
                CoreGameSignals.Instance.onTakePlayerDamage.Invoke(_enemyAttackPower);
            }
        }
    }
}
