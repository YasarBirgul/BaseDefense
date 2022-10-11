using System;
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public UnityAction onGameOpen = delegate {  };
        public UnityAction onApplicationPause = delegate {  };
        public UnityAction onApplicationQuit = delegate {  };
        public UnityAction onReadyToPlay  =delegate {  };
        
        public UnityAction onLevelInitialize = delegate { };
        public UnityAction onClearActiveLevel = delegate { };
        public UnityAction onLevelFailed = delegate { };
        public UnityAction onLevelSuccessful = delegate { };
        public UnityAction onNextLevel = delegate { };
        public UnityAction onRestartLevel = delegate { };
        public UnityAction onReset = delegate { };
        public Func<int> onGetLevelID = delegate { return 0; };
        
         public UnityAction<int> onMoneyScoreUpdate = delegate{  };
         public UnityAction<int> onDiamondScoreUpdate = delegate {  };
        public Func<bool> onHasEnoughMoney= delegate { return default;}; 
        public Func<int> onGetDiamondAmount= delegate { return default;}; 
    }
}