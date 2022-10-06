using Data.UnityObject;
using Data.ValueObject.LevelData;
using Signals;
using UnityEngine;

namespace Managers
{
    public class DataManager : MonoBehaviour
    {
        [SerializeField] 
        private CD_Level cdLevel;
        [SerializeField]
        private CD_Enemy cdEnemy;
        private void Start()
        {
           // InitData();
        }
      //  private void InitData()
      //  {
      //      cdLevel = GetLevelDatas();
      //      _levelDatas=cdLevel.LevelDatas;
      //      if (!ES3.FileExists($"LevelData{uniqueID}.es3"))
      //      {
      //          if (!ES3.KeyExists("LevelData"))
      //          {
      //              cdLevel = GetLevelDatas();
      //              _levelDatas=cdLevel.LevelDatas;
      //              Save(uniqueID);
      //          }
      //      }
      //      Load(uniqueID);
      //  }

        private CD_Level GetLevelDatas() => Resources.Load<CD_Level>("Data/CD_Level");
        
        
        #region  Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            DataInitSignals.Instance.onSaveBaseRoomData += OnSaveBaseRoomData;
            DataInitSignals.Instance.onSaveMineBaseData += OnSaveMineBaseData;
            DataInitSignals.Instance.onSaveMilitaryBaseData += OnSaveMilitaryBaseData;
            DataInitSignals.Instance.onSaveBuyablesData += OnSaveBuyablesData;
        }
        private void UnsubscribeEvents()
        {
            DataInitSignals.Instance.onSaveBaseRoomData -= OnSaveBaseRoomData;
            DataInitSignals.Instance.onSaveMineBaseData -= OnSaveMineBaseData;
            DataInitSignals.Instance.onSaveMilitaryBaseData -= OnSaveMilitaryBaseData;
            DataInitSignals.Instance.onSaveBuyablesData -= OnSaveBuyablesData;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        private void OnSaveBaseRoomData(BaseRoomData baseRoomData)
        {
            
        }
        
        private void OnSaveMineBaseData(MineBaseData mineBaseData)
        {
            
        }
        private void OnSaveMilitaryBaseData(MilitaryBaseData militaryBaseData)
        {
            
        }
        private void OnSaveBuyablesData(BuyablesData buyablesData)
        {
            
        }
        public void Save(int uniqueId)
        {
            
        }

        public void Load(int uniqueId)
        {
            
        }
    }
}