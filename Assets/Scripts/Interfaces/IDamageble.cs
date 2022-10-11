using AIBrains.EnemyBrain;
using AIBrains.SoldierBrain;
using UnityEngine;

namespace Abstract
{
    public interface IDamageble
    {
        public bool IsTaken { get; set; }
        public bool IsDead { get; set; }
        int TakeDamage(int damage);
        Transform GetTransform();
    }
}