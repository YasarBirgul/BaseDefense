﻿using Controllers.Bullet;
using Enums;
using Interfaces;
using Signals;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.SoldierBrain.States
{
    public class Attack : IState,IGetPoolObject
    {
        private SoldierAIBrain _soldierAIBrain;
        private NavMeshAgent _navMeshAgent;
        private float _timer=0.2f;
        private float _attackTime = 0.5f;
        private Animator _animator;
        private static readonly int Attacked = Animator.StringToHash("Attack");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int HasTarget = Animator.StringToHash("HasTarget");
        private BulletFireController bulletFireController;

        public Attack(SoldierAIBrain soldierAIBrain,NavMeshAgent navMeshAgent,Animator animator)
        {
            _soldierAIBrain = soldierAIBrain;
            _navMeshAgent = navMeshAgent;
            _animator = animator;
        } 
        public void Tick()
        {
            if (_soldierAIBrain.DamagebleEnemy.IsDead)
            {
                _soldierAIBrain.RemoveTarget();
            }
            if (_soldierAIBrain.EnemyTarget != null)
            {
                LookTarget();
            }
            _timer -= Time.deltaTime*_attackTime;
            if (_timer <= 0)
            {
                FireBullets();
                _timer = 0.2f;
            }
        }
        private void LookTarget()
        {
            _animator.SetFloat(Speed,_navMeshAgent.velocity.magnitude);
            
            var enemyPosition = _soldierAIBrain.EnemyTarget.transform;
            
            var lookDirection = enemyPosition.position - _soldierAIBrain.transform.position;

            Quaternion lookRotation = Quaternion.LookRotation(lookDirection, Vector3.up);

            var slerpRotation = Quaternion.Slerp(_soldierAIBrain.transform.rotation, lookRotation,3f*Time.deltaTime);

            _soldierAIBrain.transform.rotation = slerpRotation;
            
        }
        public void OnEnter()
        {
             bulletFireController = new BulletFireController(WeaponTypes.Pistol);
            _navMeshAgent.speed = 1.801268E-05f;
            _animator.SetBool(HasTarget,true);
        }
        public void OnExit()
        {
            _animator.SetBool(HasTarget,false);
        }

        public GameObject GetObject(PoolType poolName)
        {
            return PoolSignals.Instance.onGetObjectFromPool.Invoke(poolName);
        }

        private void FireBullets()
        {
            GetObject(PoolType.Pistol);
            bulletFireController.FireBullets(_soldierAIBrain.WeaponHolder);
        }
    }
}