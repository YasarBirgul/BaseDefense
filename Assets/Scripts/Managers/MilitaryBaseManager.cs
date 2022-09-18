using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject.AIData;
using Data.ValueObject.LevelData;
using Enums;
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
        private List<GameObject> SoldierList;
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
            Vector3 slotOffSet = new Vector3(_data.SlotOffSet.x, 0, _data.SlotOffSet.y);
            
            for (int i = 0; i < gridX; i++)
            {
                for (int j = 0; j < gridY; j++)
                {
                    Instantiate(_data.SlotPrefab,slotZone.transform.localPosition + slotOffSet, Quaternion.identity,
                        slotZone.transform);
                }
            }
        }
        private void InitSoldierPool()
        {
            Debug.Log("InitWorks");
            ObjectPoolManager.Instance.AddObjectPool(SoldierFactoryMethod,TurnOnSoldierAI,TurnOffSoldierAI,_tentCapacity,true);
        }
        private GameObject SoldierFactoryMethod()
        {
            return Instantiate(_soldierAIData.SoldierPrefab,TentTransfrom.position,Quaternion.identity,TentTransfrom.transform);
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
            }
            else
            {
                _isTentAvaliable= false;
            }
        }
        private void SetSlotZoneTransformsToSoldiers()
        {
            
        }
    }
}