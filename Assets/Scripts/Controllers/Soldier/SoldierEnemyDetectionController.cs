using System;
using Abstract;
using AIBrains.EnemyBrain;
using AIBrains.SoldierBrain;
using UnityEngine;

namespace Controllers.Soldier
{
    public class SoldierEnemyDetectionController : MonoBehaviour 
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables
        
        [SerializeField] private SoldierAIBrain soldierAIBrain;

        #endregion

        #region Private Variables
        
        #endregion
        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamagable damagable)) 
            
            {
                soldierAIBrain.enemyList.Add(damagable);
                if (soldierAIBrain.EnemyTarget == null) 
                {
                    soldierAIBrain.SetEnemyTargetTransform();
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IDamagable damagable))
            {
                soldierAIBrain.enemyList.Remove(damagable);
                soldierAIBrain.enemyList.TrimExcess();
                if (soldierAIBrain.EnemyTarget == null)
                {
                    soldierAIBrain.HasEnemyTarget = false;
                    if (soldierAIBrain.enemyList.Contains(damagable))
                    {
                        soldierAIBrain.SetEnemyTargetTransform();
                    }
                }
            }
            // 1.Enemies ontrigger exit yapıp tekrar enter tetikleyebilirler. Tekrar tetiklediklerinde listede iki
            // tane aynı objeden oluyor. Öldürdüğüm esnada da null bir obje oluyor. Bu sebeple listeden ontrigger exitte
            // çıkartmak lazım ki tekrar trigger edildiklerinde listeye alalım...
            
            // Bu durumda bu arkadaşlar bizim attack radius`muzun ışına çıkmış oluyorlar eğer bunlarla aramızdaki fark attack
            // radiustan büyük ise bu arkadaşların güncel pozisyonuna trigger olana kadar yürümemiz gerekiyor.
            // Chase etmek gerekiyor.
        }
    }
}