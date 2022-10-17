using Abstract;
using Enums.GameStates;
using Interfaces;
using Managers;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerDetectionController : MonoBehaviour
    {
        [SerializeField] 
        private PlayerManager manager;
        private void OnTriggerEnter(Collider other)
        {
            if (manager.CurrentAreaType == AreaType.BaseDefense) return;
            if (other.TryGetComponent(out IDamageable damagable))
            {
                if(damagable.IsDead || damagable.IsTaken) return;
                manager.EnemyList.Add(damagable);
                if ( manager.EnemyTarget == null)
                {
                    manager.SetEnemyTarget();
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damagable))
            {
                damagable.IsTaken = false;
                manager.EnemyList.Remove(damagable);
                manager.EnemyList.TrimExcess();
                if (manager.EnemyList.Count == 0)
                {
                    manager.EnemyTarget = null;
                }
            }
        }
    }
}