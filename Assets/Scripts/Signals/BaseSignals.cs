using System;
using Data.ValueObject.LevelData;
using Enums;
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class BaseSignals : MonoSingleton<BaseSignals>
    {
        public UnityAction<BaseRoomTypes> onChangeExtentionVisibility = delegate{  };
        public UnityAction<BaseRoomTypes,RoomData> onInformBaseRoom=delegate {  };
        public Func<BaseRoomTypes, RoomData> onGetRoomData= delegate { return default;};
    }
}