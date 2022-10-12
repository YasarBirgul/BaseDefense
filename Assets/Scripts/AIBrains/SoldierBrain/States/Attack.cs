using Controllers.Bullet;
using Enums;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.SoldierBrain.States
{
    public class Attack : IState
    {
        private SoldierAIBrain _soldierAIBrain;
        private NavMeshAgent _navMeshAgent;
        private float _fireRate=0.4f;
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
            if (_soldierAIBrain.DamageableEnemy.IsDead)
            {
                RemoveTarget();
            }
            if (_soldierAIBrain.EnemyTarget != null)
            {
                LookTarget();
            }
            _fireRate -= Time.deltaTime*_attackTime;
            if (_fireRate <= 0)
            {
                FireBullets();
                _fireRate = 0.4f;
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
        private void FireBullets()
        {
            bulletFireController.FireBullets(_soldierAIBrain.WeaponHolder);
        }
        
        private void SetEnemyTargetTransform()
        {
            _soldierAIBrain.EnemyTarget = _soldierAIBrain.enemyList[0].GetTransform();
            _soldierAIBrain.DamageableEnemy = _soldierAIBrain.enemyList[0];
            _soldierAIBrain.HasEnemyTarget = true;
        }
        private void EnemyTargetStatus()
        {
            if (_soldierAIBrain.enemyList.Count != 0)
            {
                SetEnemyTargetTransform();
            }
            else
            {
                _soldierAIBrain.HasEnemyTarget = false;
            }
        } 
        private void RemoveTarget()
        {
            if (_soldierAIBrain.enemyList.Count == 0) return;
            _soldierAIBrain.enemyList.RemoveAt(0);
            _soldierAIBrain.enemyList.TrimExcess();
            _soldierAIBrain.EnemyTarget = null;
            EnemyTargetStatus();
        }
    }
}