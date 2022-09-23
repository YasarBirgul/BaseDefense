﻿using Abstract;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.SoldierBrain
{
    public class Attack : IState
    {
        private SoldierAIBrain _soldierAIBrain;
        private NavMeshAgent _navMeshAgent;
        private float _timer=1f;
        private float _attackTime = 0.65f;
        private Animator _animator;
        private static readonly int Attacked = Animator.StringToHash("Attack");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int HasTarget = Animator.StringToHash("HasTarget");

        public Attack(SoldierAIBrain soldierAIBrain,NavMeshAgent navMeshAgent,Animator animator)
        {
            _soldierAIBrain = soldierAIBrain;
            _navMeshAgent = navMeshAgent;
            _animator = animator;
        } 
        public void Tick()
        {
            if (_soldierAIBrain.EnemyTarget != null)
            {
                LookTarget();
            }
            _timer -= Time.deltaTime*_attackTime;
            if (_timer <= 0 )
            {
                _soldierAIBrain.GetObjectFromPool();
                _timer = 1f;
            }
        }
        private void LookTarget()
        {
            _animator.SetFloat(Speed,_navMeshAgent.velocity.magnitude);
            var enemyPosition = _soldierAIBrain.EnemyTarget.transform;
            
            var lookDirection = enemyPosition.position - _soldierAIBrain.transform.position;

            Quaternion lookRotation = Quaternion.LookRotation(lookDirection, Vector3.up);

            var slerpRotation = Quaternion.Slerp(_soldierAIBrain.transform.rotation, lookRotation, 0.2f);

            _soldierAIBrain.transform.rotation = slerpRotation;
            
        }
        public void OnEnter()
        {
            _navMeshAgent.speed = 1.801268E-05f;
            _animator.SetBool(HasTarget,true);
        }
        public void OnExit()
        {
            _animator.SetBool(HasTarget,false);
        }
    }
}