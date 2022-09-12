using AIBrains.EnemyBrain;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class EnemyPhysicsController : MonoBehaviour
    {
        private Transform _detectedPlayer;
        private Transform _detectedMine;
        private EnemyAIBrain _enemyAIBrain;
        public bool IsPlayerInRange() => _detectedPlayer != null;
        public bool IsBombInRange() => _detectedMine != null;
        private void Awake()
        {
            _enemyAIBrain = this.gameObject.GetComponentInParent<EnemyAIBrain>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _detectedPlayer = other.GetComponentInParent<PlayerManager>().transform;
                _enemyAIBrain.Target = other.transform.parent.transform;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _detectedPlayer = null;
                gameObject.GetComponentInParent<EnemyAIBrain>().Target = null;
            }

            /*if (other.GetComponent<Mine>())
            {

            }*/
        }
        // public Vector3 GetNearestPosition(GameObject gO)
       // {
       //     return gO?.transform.position ?? Vector3.zero;
       // }
    }
}