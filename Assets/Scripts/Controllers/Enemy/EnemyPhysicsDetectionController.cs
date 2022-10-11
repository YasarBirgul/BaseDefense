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

        #region Serialized Variables,

        #endregion

        #region Private Variables
        
        private Transform _detectedPlayer;
        private Transform _detectedMine;
        private EnemyAIBrain _enemyAIBrain;
        private bool _amAIDead = false;
        #endregion
        #endregion
        public bool IsPlayerInRange() => _detectedPlayer != null;
        public bool IsBombInRange() => _detectedMine != null;
        private void Awake()
        {
            _enemyAIBrain = gameObject.GetComponentInParent<EnemyAIBrain>();
        }
        private void OnTriggerEnter(Collider other)
        { 
            if (other.CompareTag("Player"))
            {
                _detectedPlayer = other.GetComponentInParent<PlayerManager>().transform;
                _enemyAIBrain.PlayerTarget = other.transform.parent.transform;
            }
          // if (other.CompareTag("MineLure"))
          // {
          //     _detectedMine = other.transform;
          //     _enemyAIBrain.MineTarget = _detectedMine;
          // }
          // if (other.CompareTag("MineExplosion"))
          // {
          //     Debug.Log(other.tag);
          //     var damageAmount = other.transform.parent.GetComponentInParent<IDamagable>().TakeDamage(999);
          //     _enemyAIBrain.Health -= damageAmount;
          //     if (_enemyAIBrain.Health <= 0)
          //     {
          //         _amAIDead = true;
          //     }
          // }
        }
        private void OnTriggerExit(Collider other)
        { 
            if (other.CompareTag("Player"))
            {
                _detectedPlayer = null;
                _enemyAIBrain.PlayerTarget = null;
            }
         //   if (other.CompareTag("MineLure"))
         //   {
         //       _detectedMine = null;
         //       _enemyAIBrain.MineTarget = _detectedMine;
         //       _enemyAIBrain.MineTarget = null;
         //   }
        }
    }
}