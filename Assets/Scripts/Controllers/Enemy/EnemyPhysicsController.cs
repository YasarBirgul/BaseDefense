using Abstract;
using AIBrains.EnemyBrain;
using UnityEngine;

namespace Controllers
{
    public class EnemyPhysicsController : MonoBehaviour, IDamagable
    {
        [SerializeField] private EnemyAIBrain enemyAIBrain;
        private bool _amAIDead = false;
       
        public bool IsTaken { get; set; }
        public bool IsDead { get; set; }

        public int TakeDamage(int damage)
        {
            if (enemyAIBrain.Health > 0)
            {
                enemyAIBrain.Health =  enemyAIBrain.Health - damage;
                if (enemyAIBrain.Health == 0)
                {
                    _amAIDead = true;
                    enemyAIBrain.EnemyDead();
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