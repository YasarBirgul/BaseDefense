using AIBrains.EnemyBrain;
using Controllers.Player;
using Controllers.Soldier;
using Interfaces;
using UnityEngine;

namespace Controllers.AI.Enemy
{
    public class EnemyPhysicsDetectionController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        #endregion
        
        #region Serialized Variables
        [SerializeField]
        private EnemyAIBrain enemyAIBrain;

        #endregion

        #region Private Variables
        
        private Transform _detectedMine;
        
        #endregion
        
        #endregion
        private void OnTriggerEnter(Collider other)
        { 
            if (other.TryGetComponent(out PlayerPhysicsController physicsController))
            {
                PickOneTarget(other);
                enemyAIBrain.CachePlayer(physicsController);
                enemyAIBrain.CacheSoldier(null);
            }
            if (other.TryGetComponent(out SoldierHealthController soldierHealthController))
            {
                enemyAIBrain.CachePlayer(null);
                PickOneTarget(other);
                enemyAIBrain.CacheSoldier(soldierHealthController);
            }
        }
        private void OnTriggerExit(Collider other)
        { 
            if (other.TryGetComponent(out PlayerPhysicsController physicsController) )
            {
                enemyAIBrain.SetTarget(null);
                enemyAIBrain.CachePlayer(null);
            }
            if (other.TryGetComponent(out IDamageable Idamageable))
            {
                enemyAIBrain.SetTarget(null);
                enemyAIBrain.CacheSoldier(null);
            }
        }
        private void PickOneTarget(Collider other)
        {
            if (enemyAIBrain.CurrentTarget == enemyAIBrain.TurretTarget)
            {
                enemyAIBrain.SetTarget(other.transform);
            }
        }
    }
}