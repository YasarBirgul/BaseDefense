using System.Net.Configuration;
using AIBrains.EnemyBrain;
using Interfaces;
using UnityEngine;

namespace Controllers.AI.Enemy
{
    public class EnemyPhysicsController : MonoBehaviour,IDamageable
    {
        [SerializeField] 
        private EnemyAIBrain enemyAIBrain;
        public bool IsTaken { get { return _isTaken;}  set{ } }
        public bool IsDead { get; set; }

        private bool _isTaken;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamager IDamager))
            {
                if (enemyAIBrain.Health <= 0) return;
                var damage = IDamager.Damage();
                enemyAIBrain.Health -= damage;
                if (enemyAIBrain.Health == 0)
                {
                    IsDead = true;
                }
            }
        }
        public int TakeDamage(int damage)
        {
            if (enemyAIBrain.Health > 0)
            {
                enemyAIBrain.Health =- damage;
                if (enemyAIBrain.Health == 0)
                {
                    IsDead = true;
                    return enemyAIBrain.Health;
                }
                return enemyAIBrain.Health;
            }
            return 0;
        }
        public Transform GetTransform()
        {
            return transform;
        }
    }
}