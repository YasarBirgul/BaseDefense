using System;
using Data.ValueObject.AIDatas;
using Enums;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class AISignals : MonoSingleton<AISignals>
    {
        public UnityAction onSoldierActivation = delegate {  };
        public Func<WorkerType, WorkerAITypeData> onGetMoneyAIData = delegate { return null; };
        public UnityAction onSendMoneyPositionToWorkers = delegate { };
        public UnityAction<Transform> onSetMoneyPosition = delegate { };
        public UnityAction <Transform> onThisMoneyTaken = delegate { };

        public Func<Transform, Transform> onGetTransformMoney = delegate { return null; };
        public Func<Transform, Transform, Transform> OnMyMoneyTaken = delegate { return null; };
    }
}