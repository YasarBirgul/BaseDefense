using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.ValueObject.AIData
{
    [Serializable]
    public class EnemyAIData
    {
        public List<EnemyTypeData> EnemyList;
        public List<Transform> SpawnPositionList;
    }
}