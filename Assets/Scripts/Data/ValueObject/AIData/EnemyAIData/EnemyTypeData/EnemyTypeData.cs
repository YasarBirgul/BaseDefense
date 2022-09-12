using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;

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
        public List<Transform> TargetList = new List<Transform>();
        public Transform SpawnPosition;
    }
}