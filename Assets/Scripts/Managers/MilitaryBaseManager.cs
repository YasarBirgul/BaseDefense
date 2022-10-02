using System.Collections.Generic;
using AIBrains.SoldierBrain;
using Data.UnityObject;
using Data.ValueObject.AIData;
using Data.ValueObject.LevelData;
using Enums;
using Interfaces;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace Managers
{
    public class MilitaryBaseManager : MonoBehaviour,IGetPoolObject,IReleasePoolObject
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables

        [SerializeField] private Transform tentTransfrom;
        [SerializeField] private Transform slotTransform;
        
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
        } 
        private void Start()
        {
            GetObject(PoolType.SoldierAI.ToString());
        }
        private MilitaryBaseData GetBaseData() =>
            Resources.Load<CD_Level>("Data/CD_Level").LevelData[0].BaseData.MilitaryBaseData;
        private SoldierAIData GetSoldierAIData() => Resources.Load<CD_AI>("Data/CD_AI").SoldierAIData;
        public GameObject GetObject(string poolName)
        {
            var soldierAIPrefab = ObjectPoolManager.Instance.GetObject<GameObject>(poolName);
            var soldierBrain = soldierAIPrefab.GetComponent<SoldierAIBrain>();
            SetSlotZoneTransformsToSoldiers(soldierBrain);
            return soldierAIPrefab;
        }
        private void SetSlotZoneTransformsToSoldiers(SoldierAIBrain soldierBrain)
        {
            soldierBrain.GetSlotTransform(_slotTransformList[_slotTransformList.Count - 1]);
            soldierBrain.TentPosition = tentTransfrom;
            soldierBrain.FrontYardStartPosition = _data.frontYardSoldierPosition;             
            _slotTransformList.RemoveAt(_slotTransformList.Count-1);
            _slotTransformList.TrimExcess();
        }
        public void ReleaseObject(GameObject obj, string poolName)
        {
            ObjectPoolManager.Instance.ReturnObject(obj,poolName);
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
        public void GetStackPositions(List<Vector3> gridPositionData)
        {
            for (int i = 0; i < gridPositionData.Count; i++)
            {
               _slotTransformList.Add(gridPositionData[i]);
              var obj=  Instantiate(_data.SlotPrefab,gridPositionData[i],quaternion.identity,slotTransform);
            }
        }
        
    }
}