using System;
using System.Collections.Generic;
using System.Linq;
using AI.MinerAI;
using Data.UnityObjects;
using Data.ValueObjects;
using Enum;
using FrameworkGoat;
using Signals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class MineBaseManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        

        #endregion

        #region Serialized Variables

        private Transform _instantiationPosition;
        private Transform _gemHolderPosition;
        

        #endregion

        #region Private Variables

        private int _currentLevel; //LevelManager uzerinden cekilecek
        private int _gemCapacity;
        private int _mineBaseCapacity;
        private Transform _currentMineTarget;
        private int _currentGemAmount;
        private int _currentWorkerAmount;
        private int _mineCartCapacity;
        private int _maxWorkerAmount;
        public float GemCollectionOffset;
        private Dictionary<MinerAIBrain, GameObject> _mineWorkers=new Dictionary<MinerAIBrain, GameObject>();
        private MineBaseData _mineBaseData;
        

        #endregion

        #endregion
        private void Awake()
        {
            _mineBaseData=GetMineBaseData();
            AssignDataValues();
          
        }

        private void Start()
        {
            InstantiateAllMiners();
            AssignMinerValuesToDictionary();
        }
       

        private void InstantiateAllMiners()
        {
            for (int index = 0; index < _currentWorkerAmount; index++)
            {
                GameObject _currentObject=ObjectPoolManager.Instance.GetObject<GameObject>("MinerAI");
                MinerAIBrain _currentMinerAIBrain=_currentObject.GetComponent<MinerAIBrain>();
                _mineWorkers.Add(_currentMinerAIBrain,_currentObject);
            }
        }

        private void AssignMinerValuesToDictionary()
        {
            for (int index = 0; index < _mineWorkers.Count; index++)
            {
                _mineWorkers.ElementAt(index).Key.GemCollectionOffset=GemCollectionOffset;
                _mineWorkers.ElementAt(index).Key.GemHolder= _gemHolderPosition;
            }
            
        }

        private void AssignDataValues()
        {
               _gemCapacity =_mineBaseData.DiamondCapacity;
                _currentGemAmount =_mineBaseData.CurrentDiamondAmount;
                _currentWorkerAmount =_mineBaseData.CurrentWorkerAmount;
                GemCollectionOffset=_mineBaseData.GemCollectionOffset;
                _maxWorkerAmount=_mineBaseData.MaxWorkerAmount;
                _mineBaseCapacity=_mineBaseData.DiamondCapacity;
                _mineCartCapacity=_mineBaseData.MineCartCapacity;
                _gemHolderPosition = _mineBaseData.GemHolderPosition;
                _instantiationPosition = _mineBaseData.InstantiationPosition;


        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            MineBaseSignals.Instance.onGetRandomMineTarget += GetRandomMineTarget;
            MineBaseSignals.Instance.onGetGemHolderPos += OnGetGemHolderPos;
        }
       

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void UnSubscribeEvents()
        {
            MineBaseSignals.Instance.onGetRandomMineTarget -= GetRandomMineTarget;
            MineBaseSignals.Instance.onGetGemHolderPos -= OnGetGemHolderPos;
        }

        private Transform OnGetGemHolderPos()
        {
            return _gemHolderPosition;
        }

        #endregion

        public Tuple<Transform,GemMineType> GetRandomMineTarget()
        {
            int randomMineTargetIndex=Random.Range(0, _mineBaseData.MinePlaces.Count + _mineBaseData.CartPlaces.Count);
            return randomMineTargetIndex>= _mineBaseData.MinePlaces.Count
                ? Tuple.Create(_mineBaseData.CartPlaces[randomMineTargetIndex % _mineBaseData.CartPlaces.Count],GemMineType.Cart)
                :Tuple.Create(_mineBaseData.MinePlaces[randomMineTargetIndex],GemMineType.Mine);//Tuple ile enum donecek maden tipine gore animasyon degisecek stateler uzerinden
        }


        public MineBaseData GetMineBaseData() => Resources.Load<CD_Level>("Data/CD_Level").LevelDatas[_currentLevel].BaseData.MineBaseData;
    }
}