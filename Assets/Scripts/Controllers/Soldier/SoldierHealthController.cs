using System;
using Abstract;
using AIBrains.SoldierBrain;
using Interfaces;
using UnityEngine;

namespace Controllers.Soldier
{ 
    public class SoldierHealthController : AInteractable, IDamageable
    {
        [SerializeField] 
        private SoldierAIBrain soldierAIBrain;
        public bool IsTaken { get; set; }
        public bool IsDead { get; set; }

        private void OnEnable()
        {
            IsDead = false;
        }
        public int TakeDamage(int damage)
        { 
            soldierAIBrain.Health -= damage;
            if (soldierAIBrain.Health <= 0)
            {
                IsDead = true;
            }
            return soldierAIBrain.Health;
        }
        public Transform GetTransform()
        {
            return transform;
        }
    }
}