using System;
using Enums.Turret;
using Extentions;
using UnityEngine;
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
        
        public UnityAction onEnterTurret = delegate {  };

        public UnityAction onLevel = delegate {  };

        public UnityAction onFinish = delegate {  };
        
        public UnityAction onCharacterInputRelease = delegate {  };
        
        public UnityAction<TurretLocationType,GameObject> onSetCurrentTurret = delegate(TurretLocationType arg0, GameObject o) {  };
        
        public Func<int> onGetHealthValue= delegate { return default;};
        public UnityAction<int> onHealthUpgrade  =delegate {  };
        
        public UnityAction<int> onTakePlayerDamage= delegate {  };
        public UnityAction<int> onTakeSoldierDamage = delegate {  };
        
        public UnityAction onResetPlayerStack = delegate {  };
    }
}