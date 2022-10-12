using Abstract;
using UnityEngine;

namespace Controllers
{
    public class TurretDetectionController : MonoBehaviour
    {
        [SerializeField] private TurretShootController shootController;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out IDamageable damageable)) return;
            print("ENEMY!");
            if (!damageable.IsTaken)
                shootController.EnemyInRange(damageable.GetTransform().gameObject);
            shootController.ShootTheTarget();
        }
        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out IDamageable damageable)) return;
            print("ENEMY!");
            shootController.EnemyOutOfRange(damageable.GetTransform().gameObject);
            damageable.IsTaken = false;

        }
    }
}