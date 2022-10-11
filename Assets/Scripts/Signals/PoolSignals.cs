using System;
using Enums;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class PoolSignals : MonoSingleton<PoolSignals>
    {
        public Func<PoolType,GameObject> onGetObjectFromPool = delegate { return null;};
        public UnityAction<PoolType,GameObject> onReleaseObjectFromPool = delegate {  };
    }
}