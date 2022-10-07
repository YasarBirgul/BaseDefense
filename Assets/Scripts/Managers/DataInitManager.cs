using System;
using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject.LevelData;
using Interfaces;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class DataInitManager : MonoBehaviour,ISaveable
    
    {
        #region Self Variables
        
        #region Public Variables

        #endregion
        
        #region Serialized Variables
        
        [SerializeField] 
        private List<LevelData> levelDatas = new List<LevelData>();
        
        [ShowInInspector] 
        private CD_Level cdLevel;
        
        // [SerializeField] private CD_Enemy cdEnemy;

        #endregion
        
        #region Private Variables
        
        private int _levelID;
        private int _uniqueID = 345;
        
        private LevelData _levelData;
        private BaseRoomData _baseRoomData;
        private MineBaseData _mineBaseData;
        private MilitaryBaseData _militaryBaseData;
        private  BuyablesData _buyablesData;

        #endregion
        
        #endregion

        private void Awake()
        {
            cdLevel=GetLevelDatas();
        }

        private CD_Level GetLevelDatas() => Resources.Load<CD_Level>("Data/CD_Level");
        private void Start()
        {
            InitData();
            CoreGameSignals.Instance.onLevelInitialize?.Invoke();
        }
        #region InitData
        private void InitData()
        {   
            cdLevel = GetLevelDatas();
            _levelID = cdLevel.LevelId;
            levelDatas=cdLevel.LevelDatas;
            if (!ES3.FileExists($"LevelData{_uniqueID}.es3"))
            {
                if (!ES3.KeyExists("LevelData"))
                {
                    cdLevel = GetLevelDatas();
                    _levelID = cdLevel.LevelId;
                    levelDatas=cdLevel.LevelDatas;
                    Save(_uniqueID);
                    Debug.Log(_uniqueID);
                }
            }
            Load(_uniqueID);
        }

        #endregion

        #region Event Subscriptions
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            DataInitSignals.Instance.onSaveLevelID += OnSyncLevelID;
            CoreGameSignals.Instance.onLevelInitialize += OnSyncLevel;
            DataInitSignals.Instance.onSaveBaseRoomData += SyncBaseRoomDatas;
            DataInitSignals.Instance.onSaveMineBaseData += SyncMineBaseDatas;
            DataInitSignals.Instance.onSaveMilitaryBaseData += SyncMilitaryBaseData;
            DataInitSignals.Instance.onSaveBuyablesData += SyncBuyablesData;

            DataInitSignals.Instance.onLoadMilitaryBaseData += OnLoadMilitaryBaseData;
            DataInitSignals.Instance.onLoadBaseRoomData += OnLoadBaseRoomData;
            DataInitSignals.Instance.onLoadBuyablesData += OnLoadBuyablesData;
            DataInitSignals.Instance.onLoadMineBaseData += OnLoadMineBaseData;
            CoreGameSignals.Instance.onApplicationQuit += OnApplicationQuit;
        }

        private void UnsubscribeEvents()
        {
            DataInitSignals.Instance.onSaveLevelID -= OnSyncLevelID;
            CoreGameSignals.Instance.onLevelInitialize -= OnSyncLevel;
            DataInitSignals.Instance.onSaveBaseRoomData -= SyncBaseRoomDatas;
            DataInitSignals.Instance.onSaveMineBaseData -= SyncMineBaseDatas;
            DataInitSignals.Instance.onSaveMilitaryBaseData -= SyncMilitaryBaseData;
            DataInitSignals.Instance.onSaveBuyablesData -= SyncBuyablesData;
            
            DataInitSignals.Instance.onLoadMilitaryBaseData -= OnLoadMilitaryBaseData;
            DataInitSignals.Instance.onLoadBaseRoomData -= OnLoadBaseRoomData;
            DataInitSignals.Instance.onLoadBuyablesData -= OnLoadBuyablesData;
            DataInitSignals.Instance.onLoadMineBaseData -= OnLoadMineBaseData;
            CoreGameSignals.Instance.onApplicationQuit -= OnApplicationQuit;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        #region ManagersData
        private void SendDataManagers()
        {   
            DataInitSignals.Instance.onLoadLevelID?.Invoke(_levelID);
        }
        private MilitaryBaseData OnLoadMilitaryBaseData()
        {
            return _militaryBaseData;
        }
        private BaseRoomData OnLoadBaseRoomData()
        {
            return _baseRoomData;
        }
        private MineBaseData OnLoadMineBaseData()
        {
            return _mineBaseData;
        }
        private BuyablesData OnLoadBuyablesData()
        {
            return _buyablesData;
        }
        #endregion
        
        #region Level Save - Load 

        public void Save(int uniqueId)
        {
            CD_Level cdLevel = new CD_Level(_levelID, levelDatas);
            SaveLoadSignals.Instance.onSaveGameData.Invoke(cdLevel,uniqueId);
        }
        public void Load(int uniqueId)
        {
            CD_Level cdLevel = SaveLoadSignals.Instance.onLoadGameData.Invoke(this.cdLevel.GetKey(), uniqueId);
            _levelID = cdLevel.LevelId;
            levelDatas = cdLevel.LevelDatas;
            _baseRoomData = cdLevel.LevelDatas[_levelID].BaseData.BaseRoomData;
            _mineBaseData = cdLevel.LevelDatas[_levelID].BaseData.MineBaseData;
            _militaryBaseData = cdLevel.LevelDatas[_levelID].BaseData.MilitaryBaseData;
            _buyablesData = cdLevel.LevelDatas[_levelID].BaseData.BuyablesData;
        }
        #endregion
        
        private void OnSyncLevel() 
        {
            SendDataManagers();
        }

        #region Data Sync

        private void OnSyncLevelID(int levelID)
        {
            cdLevel.LevelId = levelID;
        }
        private void SyncBaseRoomDatas(BaseRoomData baseRoomData)
        {
            cdLevel.LevelDatas[_levelID].BaseData.BaseRoomData = baseRoomData;
        }

        private void SyncMineBaseDatas(MineBaseData mineBaseData)
        {
            cdLevel.LevelDatas[_levelID].BaseData.MineBaseData = mineBaseData;
        }

        private void SyncMilitaryBaseData(MilitaryBaseData militaryBaseData)
        {
            cdLevel.LevelDatas[_levelID].BaseData.MilitaryBaseData = militaryBaseData;
        }
        
        private void SyncBuyablesData(BuyablesData buyablesData)
        {
            cdLevel.LevelDatas[_levelID].BaseData.BuyablesData = buyablesData;
        }
        #endregion

        [Button]
        private void OnSave()
        {
            Save(_uniqueID);
        }
        private void OnApplicationQuit()
        {
            Save(_uniqueID);
        }
    }
}