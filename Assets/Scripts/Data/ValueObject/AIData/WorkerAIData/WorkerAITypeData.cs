using System;
using Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data.ValueObject.AIData.WorkerAIData
{
    [Serializable]
    public class WorkerAITypeData
    {
        public WorkerType WorkerType;
       
        [HideIf("WorkerType", WorkerType.SoldierAI)]
        public int CapacityOrDamage;
        
        [HideIf("WorkerType", WorkerType.SoldierAI)]
        public float Speed;
        
        [HideIf("WorkerType", WorkerType.SoldierAI)]
        public Transform StartTarget;
        
        [ShowIf("WorkerType", WorkerType.SoldierAI)]
        public SoldierAIData SoldierAIData;
    }

}