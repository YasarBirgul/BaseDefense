using System;
using System.Collections.Generic;
using AIBrains.SoldierBrain;
using Data.UnityObject;
using Data.ValueObject.AIData;
using Data.ValueObject.LevelData;
using Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class MilitaryBaseManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables

        [SerializeField] Transform slotZone;
        [SerializeField] private Transform TentTransfrom;
        
        #endregion

        #region Private Variables
        
        private MilitaryBaseData _data;
        private SoldierAIData _soldierAIData;
        private bool _isBaseAvaliable;
        private bool _isTentAvaliable;
        private int _totalAmount;
        private int _soldierAmount;
        private List<GameObject> _soldierList = new List<GameObject>();
        [ShowInInspector] private List<Vector3> _slotTransformList = new List<Vector3>();
        private int _tentCapacity;
        #endregion

        #endregion

        private void Awake()
        {
            _data = GetBaseData();
            _soldierAIData = GetSoldierAIData();
            Init();
            SetWaitSlotsGrid();
            InitSoldierPool();
        } 
        private void Start()
        {
            GetObjectFromPool();
        }
        private void Init()
        {
            _tentCapacity = _data.TentCapacity;
        }
        private MilitaryBaseData GetBaseData() =>
            Resources.Load<CD_Level>("Data/CD_Level").LevelData[0].BaseData.MilitaryBaseData;
        private SoldierAIData GetSoldierAIData() => Resources.Load<CD_AI>("Data/CD_AI").SoldierAIData;
        private void SetWaitSlotsGrid()
        {
            int gridX = (int) _data.SlotsGrid.x;
            int gridY = (int) _data.SlotsGrid.y;
            Vector3 slotPivot =
                new Vector3(slotZone.transform.localScale.x / 2, 0, slotZone.transform.localScale.z / 2);
            for (int i = 0; i < gridX; i++)
            {
                for (int j = 0; j < gridY; j++)
                {
                    var SlotPosition = new Vector3(i*_data.SlotOffSet.x, 0, j*_data.SlotOffSet.y) + slotPivot;
                    var slotPositions = slotZone.transform.localPosition + SlotPosition;
                    Instantiate(_data.SlotPrefab,slotPositions, Quaternion.identity,slotZone.transform);
                    _slotTransformList.Add(slotPositions);
                }
            }
        } 
        private void InitSoldierPool()
        {
            ObjectPoolManager.Instance.AddObjectPool(SoldierFactoryMethod,TurnOnSoldierAI,TurnOffSoldierAI,_soldierAIData.SoldierType.ToString(),_tentCapacity,true);
        } 
        private GameObject SoldierFactoryMethod()
        {
            return Instantiate(_soldierAIData.SoldierPrefab,TentTransfrom.position,Quaternion.identity,TentTransfrom.transform);
        }
        private void GetObjectFromPool()
        {
            var soldierAIPrefab = ObjectPoolManager.Instance.GetObject<GameObject>(_soldierAIData.SoldierType.ToString());
            var soldierBrain = soldierAIPrefab.GetComponent<SoldierAIBrain>();
            SetSlotZoneTransformsToSoldiers(soldierBrain);
        }
        private void SetSlotZoneTransformsToSoldiers(SoldierAIBrain soldierBrain)
        {
            soldierBrain.GetSlotTransform(_slotTransformList[_slotTransformList.Count - 1]);
            soldierBrain.TentPosition = TentTransfrom;
            _slotTransformList.RemoveAt(_slotTransformList.Count-1);
            _slotTransformList.TrimExcess();
        }
        private void TurnOnSoldierAI(GameObject soldierPrefab)
        {
           soldierPrefab.SetActive(true);
        }
        private void TurnOffSoldierAI(GameObject soldierPrefab)
        {
            soldierPrefab.SetActive(false);
        }
        private void ReleaseSoldierObject(GameObject soldierPrefab,SoldierType soldierType)
        {
            ObjectPoolManager.Instance.ReturnObject(soldierPrefab,soldierType.ToString());
        }
        public void UpdateTotalAmount(int Amount)
        {
            if(!_isBaseAvaliable) return;
            if (_totalAmount < _data.BaseCapacity)
            {
                _totalAmount += Amount;
            }
            else
            {
                _isBaseAvaliable = false;
            }
        }
        public void UpdateSoldierAmount(int Amount)
        {
            if(!_isTentAvaliable) return;
            if (_soldierAmount < _data.TentCapacity)
            {
                _soldierAmount += Amount;
               // GetObjectFromPool();
            }
            else
            {
                _isTentAvaliable= false;
            }
        }
    }
}