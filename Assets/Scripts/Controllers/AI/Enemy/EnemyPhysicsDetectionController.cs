using AIBrains.EnemyBrain;
using Controllers.Player;
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