using Enums.Input;
using Extentions;
using Keys;
using UnityEngine.Events;

namespace Signals
{ 
    public class InputSignals : MonoSingleton<InputSignals>
    { 
        public UnityAction<HorizontalInputParams> onInputDragged = delegate{  };
        
        public UnityAction<HorizontalInputParams> onJoystickInputDraggedforTurret = delegate(HorizontalInputParams arg0) {  };
        
        public UnityAction<InputHandlers> onInputHandlerChange = delegate(InputHandlers arg0) {  };
    }
}