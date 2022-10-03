using Abstract;
using AIBrains.EnemyBrain;
using UnityEngine;

namespace Controllers
{
    public class EnemyPhysicsController : MonoBehaviour, IDamagable
    {
        [SerializeField] 
        private EnemyAIBrain enemyAIBrain;
        public bool IsTaken { get; set; }
        public bool IsDead { get; set; }

        public int TakeDamage(int damage)
        {
            if (enemyAIBrain.Health > 0)
            {
                enemyAIBrain.Health =  enemyAIBrain.Health - damage;
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