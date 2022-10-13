using System;
using Enums;
using Sirenix.OdinInspector;

namespace Data.ValueObject.AIData.WorkerAIData
{
    [Serializable]
    public class WorkerAITypeData
    {
        public WorkerType WorkerType;
       
        [HideIf("WorkerType", WorkerType.SoldierAI)]
        public int CapacityOrDamage;
        
        [HideIf("WorkerType", WorkerType.SoldierAI)]
        public float MaxSpeed;
        
        [HideIf("WorkerType", WorkerType.SoldierAI)]
        public float MinSpeed;
        
        [HideIf("WorkerType", WorkerType.SoldierAI)]
        public int CurrentWorkerValue;
        
        [ShowIf("WorkerType", WorkerType.SoldierAI)]
        public SoldierAIData SoldierAIData;
    }

}