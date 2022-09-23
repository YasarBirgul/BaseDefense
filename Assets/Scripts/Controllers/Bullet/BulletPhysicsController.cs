using Abstract;
using AIBrains.SoldierBrain;
using UnityEngine;

namespace Controllers.Bullet
{
    public class BulletPhysicsController : MonoBehaviour
    {
        private int bulletDamage = 20;
        public SoldierAIBrain soldierAIBrain;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamagable damagable))
            {
                var health = damagable.TakeDamage(bulletDamage);
                if (health <= 0)
                {
                    soldierAIBrain.RemoveTarget();
                }
                else
                {
                    soldierAIBrain.EnemyTargetStatus();
                }
            }
        }
    }
}