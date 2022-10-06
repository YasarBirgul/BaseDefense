using Data.ValueObject.LevelData;
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class DataInitSignals : MonoSingleton<DataInitSignals>
    { 
        
        public UnityAction<BaseRoomData> onSaveBaseRoomData = delegate {  };
        public UnityAction<MineBaseData> onSaveMineBaseData = delegate {  };
        public UnityAction<MilitaryBaseData> onSaveMilitaryBaseData = delegate{  };
        public UnityAction<BuyablesData> onSaveBuyablesData = delegate(BuyablesData arg0) {  };
        public UnityAction<int> onSaveLevelID = delegate(int arg0) {  };
        
        public UnityAction<BaseRoomData> onLoadBaseRoomData= delegate {  };
        public UnityAction<MineBaseData> onLoadMineBaseData = delegate {  };
        public UnityAction<MilitaryBaseData> onLoadMilitaryBaseData = delegate{  };
        public UnityAction<BuyablesData> onLoadBuyablesData = delegate {  };
        public UnityAction<int> onLoadLevelID = delegate(int arg0) {  };
    }
}