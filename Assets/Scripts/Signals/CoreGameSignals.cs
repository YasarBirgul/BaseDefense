using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public UnityAction onGameOpen = delegate {  };
        public UnityAction onApplicationPause = delegate {  };
        public UnityAction onApplicationQuit = delegate {  };
        public UnityAction onPlay  =delegate {  };
    }
}