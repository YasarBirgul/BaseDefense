using System;
using System.Collections.Generic;
using System.Linq;
using AI.MinerAI;
using Enum;
using Enums;
using Interfaces;
using Signals;
using UnityEngine;
using MineBaseData = Data.ValueObject.LevelData.MineBaseData;
using Random = UnityEngine.Random;

namespace Managers
{
    public class MineBaseManager : MonoBehaviour, IGetPoolObject
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables

        [SerializeField]
        private Transform instantiationPosition;
        [SerializeField]
        private Transform gemHolderPosition;
        [SerializeField]
        private List<Transform> minePlaces;
        [SerializeField]
        private List<Transform> cartPlaces;

        #endregion

        #region Private Variables
        
        private Dictionary<MinerAIBrain, GameObject> _mineWorkers=new Dictionary<MinerAIBrain, GameObject>();
        
        private MineBaseData _mineBaseData;
        
        #endregion

        #endregion
        private void Awake()
        {
            _mineBaseData = DataInitSignals.Instance.onLoadMineBaseData.Invoke();
        }
        private void Start()
        {
            InstantiateAllMiners();
            AssignMinerValuesToDictionary();
        }
        private void InstantiateAllMiners()
        {
            for (int index = 0; index < _mineBaseData.CurrentWorkerAmount; index++)
            {
                GameObject _currentObject = GetObject(PoolType.MinerWorker);
                _currentObject.transform.position = instantiationPosition.position;
                MinerAIBrain _currentMinerAIBrain=_currentObject.GetComponent<MinerAIBrain>();
                _mineWorkers.Add(_currentMinerAIBrain,_currentObject);
            }
        }

        private void AssignMinerValuesToDictionary()
        {
            for (int index = 0; index < _mineWorkers.Count; index++)
            {
                _mineWorkers.ElementAt(index).Key.GemCollectionOffset=_mineBaseData.GemCollectionOffset;
                _mineWorkers.ElementAt(index).Key.GemHolder= gemHolderPosition;
            }
            
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
            return gemHolderPosition;
        }

        #endregion
        private Tuple<Transform,GemMineType> GetRandomMineTarget()
       {
           int randomMineTargetIndex=Random.Range(0, minePlaces.Count + cartPlaces.Count);
           return randomMineTargetIndex>= minePlaces.Count
               ? Tuple.Create(cartPlaces[randomMineTargetIndex % cartPlaces.Count],GemMineType.Cart)
               :Tuple.Create(minePlaces[randomMineTargetIndex],GemMineType.Mine);//Tuple ile enum donecek maden tipine gore animasyon degisecek stateler uzerinden
       }
       public GameObject GetObject(PoolType poolName)
       {
           return PoolSignals.Instance.onGetObjectFromPool.Invoke(poolName);
       }
    }
}