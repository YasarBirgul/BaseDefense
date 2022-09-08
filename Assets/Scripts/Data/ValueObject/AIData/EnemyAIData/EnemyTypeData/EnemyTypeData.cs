using System;
using Enums.Enemy;

namespace Data.ValueObject.AIData
{
    [Serializable]
    public class EnemyTypeData
    {
        public EnemyTypes EnemyType;
        public int Health;
        public int Damage;
        public float AttackRange;
        public float AttackSpeed;
        public float MoveSpeed;
        public float ChaseSpeed;
    }
}