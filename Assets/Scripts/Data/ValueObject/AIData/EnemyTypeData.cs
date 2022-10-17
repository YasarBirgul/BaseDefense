using System;
using Abstract;
using Enums;
using UnityEngine;

namespace Data.ValueObject.AIData
{
    [Serializable]
    public class EnemyTypeData : AEnemy
    {
        public float ChaseSpeed;
        public float MoveSpeed;
        public float NavMeshRadius;
        public float NavMeshHeight;
        public EnemyTypeData(EnemyTypes enemyType,int health,int damage,float attackRange,float attackSpeed,Vector3 scaleSize,Color bodyColor)
            : base(enemyType, health, damage, attackRange, attackSpeed, scaleSize, bodyColor)
        {
            
        }
    }
}