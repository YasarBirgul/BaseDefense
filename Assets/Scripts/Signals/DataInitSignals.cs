using System;
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
        
        public UnityAction<int> onLoadLevelID = delegate(int arg0) {  };
        
        public Func<MilitaryBaseData> onLoadMilitaryBaseData = delegate { return null; };
        public Func<BaseRoomData> onLoadBaseRoomData = delegate { return null; };
        public Func<BuyablesData> onLoadBuyablesData = delegate { return null; };
        public Func<MineBaseData> onLoadMineBaseData = delegate { return null;};
        
    }
}