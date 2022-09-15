using Abstract;
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
        private bool _amAIDead = false;
        public bool IsPlayerInRange() => _detectedPlayer != null;
        public bool IsBombInRange() => _detectedMine != null;
        public bool AmIdead() => _amAIDead;
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
            if (other.CompareTag("MineLure"))
            {
                _detectedMine = other.transform;
                _enemyAIBrain.MineTarget = _detectedMine;
            }
            if (other.CompareTag("Bullet"))
            {
                var damageAmount = other.GetComponent<IDamagable>().GetDamage();
                _enemyAIBrain.Health -= damageAmount;
                if (_enemyAIBrain.Health <= 0)
                {
                    _amAIDead = true;
                }
            }
            if (other.CompareTag("MineExplosion"))
            {
                Debug.Log(other.tag);
                var damageAmount = other.transform.parent.GetComponentInParent<IDamagable>().GetDamage();
                _enemyAIBrain.Health -= damageAmount;
                if (_enemyAIBrain.Health <= 0)
                {
                    _amAIDead = true;
                    Debug.Log(_amAIDead);
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _detectedPlayer = null;
                gameObject.GetComponentInParent<EnemyAIBrain>().PlayerTarget = null;
            }
            if (other.CompareTag("MineLure"))
            {
                _detectedMine = null;
                _enemyAIBrain.MineTarget = _detectedMine;
                _enemyAIBrain.MineTarget = null;
            }
        }
    }
}