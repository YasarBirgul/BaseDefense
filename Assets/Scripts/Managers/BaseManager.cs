using System.Collections.Generic;
using Controllers;
using Data.ValueObject.LevelData;
using Enums;
using Enums.Turret;
using Signals;
using UnityEngine;

namespace Managers
{
    public class BaseManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] 
        private BaseExtentionController baseExtentionController;
        [SerializeField]
        
        #endregion
    
        #region Public Variables
        
        public BaseRoomData Data;

        #endregion

        #region Private Variables

        #endregion
        
        #endregion

        #region Event Subscription
        private void Awake()
        {
            Data = GetData();
            SetUpExistingRooms();
        }
        private BaseRoomData GetData()
        {
            return DataInitSignals.Instance.onLoadBaseRoomData.Invoke();
        }
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            BaseSignals.Instance.onChangeExtentionVisibility += OnChangeVisibility;
        }
        private void UnsubscribeEvents()
        {
            BaseSignals.Instance.onChangeExtentionVisibility -= OnChangeVisibility;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        private void OnChangeVisibility(BaseRoomTypes baseRoomType)
        {
            ChangeVisibility(baseRoomType);
        }
        private void ChangeVisibility(BaseRoomTypes baseRoomType)
        {
            baseExtentionController.ChangeExtentionVisibility(baseRoomType);
        }
        private void SetUpExistingRooms()
        {
            for (int i = 0; i < Data.RoomDatas.Count ; i++)
            {
                if (Data.RoomDatas[i].AvailabilityType == AvailabilityType.Unlocked)
                {
                    Debug.Log(Data.RoomDatas[i].BaseRoomType);
                    ChangeVisibility(Data.RoomDatas[i].BaseRoomType);
                } 
            }
        }
        private void ChangeRoomStatus(BaseRoomTypes roomTypes)
        {
            
        }
    }
}