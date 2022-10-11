using AIBrains.EnemyBrain;
using Managers;
using UnityEngine;

namespace Controllers
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
        
        private Transform _detectedPlayer;
        private Transform _detectedMine;
        private bool _amAIDead = false;
        #endregion
        #endregion
        public bool IsPlayerInRange() => _detectedPlayer != null;
        public bool IsBombInRange() => _detectedMine != null;
        private void OnTriggerEnter(Collider other)
        { 
            if (other.TryGetComponent(out PlayerPhysicsController physicsController))
            {
                _detectedPlayer = physicsController.transform;
                enemyAIBrain.SetTarget(other.transform.parent);
            }
        }
        private void OnTriggerExit(Collider other)
        { 
            if (other.TryGetComponent(out PlayerPhysicsController physicsController))
            {
                enemyAIBrain.CurrentTarget = null;
                enemyAIBrain.SetTarget(null);
            }
        }
    }
}