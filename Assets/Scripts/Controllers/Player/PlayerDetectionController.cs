using Abstract;
using Managers;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerDetectionController : MonoBehaviour
    {
        [SerializeField] private PlayerManager manager;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamagable damagable))
            {
                  if(damagable.IsTaken) return;
                  manager.EnemyList.Add(damagable);
                  if (manager.EnemyTarget == null)
                  {
                      damagable.IsTaken = true;
                      manager.SetEnemyTarget();
                  }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IDamagable damagable))
            {
                damagable.IsTaken = false;
                manager.EnemyList.Remove(damagable);
                manager.EnemyList.TrimExcess();
                if (manager.EnemyList.Count == 0)
                {
                    manager.EnemyTarget = null;
                    manager.HasEnemyTarget = false;
                    manager.DamagableEnemy = null;
                }
            }
        }
    }
}