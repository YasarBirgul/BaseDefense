using Abstract;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

namespace AIBrains.SoldierBrain
{
    public class Attack : IState
    {
        private SoldierAIBrain _soldierAIBrain;
        private NavMeshAgent _navMeshAgent;
        private float _timer=1f;
        private float _attackTime = 1.7f;
        private Animator _animator;
        private static readonly int Attacked = Animator.StringToHash("Attack");

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
                if (_soldierAIBrain.EnemyTarget != null)
                {
                     var health = _soldierAIBrain.enemyList[0].TakeDamage(20);
                     if (health <= 0)
                     {
                         _soldierAIBrain.enemyList.RemoveAt(0);
                         _soldierAIBrain.enemyList.TrimExcess();
                         _soldierAIBrain.EnemyTarget = null;
                         _soldierAIBrain.EnemyTargetStatus();
                     }
                     /*  _soldierAIBrain.EnemyTarget = null; */
                } 
                 // else if (_soldierAIBrain.enemyList.Count != 0) 
                 // {
                 //     Debug.Log("  timer out " );
                 //     _soldierAIBrain.enemyList.RemoveAt(0);
                 //     _soldierAIBrain.enemyList.TrimExcess();
                 //     if (_soldierAIBrain.enemyList.Count != 0)
                 //     {
                 //         _soldierAIBrain.SetEnemyTargetTransform();
                 //     }
                 // }
                 else
                 {
                     _soldierAIBrain.EnemyTargetStatus();
                 }
                 _timer = 1f;
            }
        }
        private void LookTarget()
        {
            var enemyPosition = _soldierAIBrain.EnemyTarget.transform;
            
            var lookDirection = enemyPosition.position - _soldierAIBrain.transform.position;

            Quaternion lookRotation = Quaternion.LookRotation(lookDirection, Vector3.up);

            var slerpRotation = Quaternion.Slerp(_soldierAIBrain.transform.rotation, lookRotation, 0.2f);

            _soldierAIBrain.transform.rotation = slerpRotation;
            
        }
        public void OnEnter()
        {
            Debug.Log("AttackEnter");
            _animator.speed = 0;
            _animator.SetTrigger(Attacked);
        }
        public void OnExit()
        {
            _animator.ResetTrigger(Attacked);
        }
    }
}