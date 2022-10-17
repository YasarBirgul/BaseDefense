using System;
using Concrete;
using Data.ValueObject.AIData.WorkerAIData;
using Enums;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class AISignals : MonoSingleton<AISignals>
    {
      
        public Func<WorkerType, WorkerAITypeData> onGetMoneyAIData = delegate { return null; };
        public UnityAction onSendMoneyPositionToWorkers = delegate { };
        public UnityAction<StackableBaseMoney> onSetMoneyPosition = delegate { };
        public UnityAction onThisMoneyTaken = delegate { };

        public Func<Transform, Transform> onGetTransformMoney = delegate { return null; };
        public Func<Transform, Transform, Transform> OnMyMoneyTaken = delegate { return null; };
        
        public Func<Transform> getSpawnTransform = delegate { return default;};
        public Func<Transform> getRandomTransform =delegate { return default;};
        
        public UnityAction onSoldierActivation = delegate {  };
        public UnityAction onSoldierAmountUpgrade  =delegate {  };
    }
}