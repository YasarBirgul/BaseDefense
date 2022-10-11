﻿using Abstract;
using AIBrains.SoldierBrain;
using UnityEngine;

namespace Controllers.Soldier
{
    public class SoldierDetectionController : MonoBehaviour 
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
            if (other.TryGetComponent(out IDamageable damagable))
            {
                if(damagable.IsTaken) return;
                damagable.IsTaken = true;
                soldierAIBrain.enemyList.Add(damagable);
                if (soldierAIBrain.EnemyTarget == null)
                {
                    soldierAIBrain.SetEnemyTargetTransform();
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damagable))
            {
                if (soldierAIBrain.enemyList.Count == 0)
                {
                    soldierAIBrain.EnemyTarget = null;
                }
                damagable.IsTaken = false;
                soldierAIBrain.enemyList.Remove(damagable);
                soldierAIBrain.enemyList.TrimExcess();
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