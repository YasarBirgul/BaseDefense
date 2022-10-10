using System.Linq;
using Controllers;
using Data.ValueObject.LevelData;
using Enums;
using Enums.Turret;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class BaseManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] 
        private BaseExtentionController baseExtentionController;
        
        #endregion
    
        #region Public Variables
        
        private BaseRoomData _data=new BaseRoomData();

        #endregion

        #region Private Variables

        #endregion
        
        #endregion
        private void Awake()
        {
            _data = GetData();
            SetUpExistingRooms();
        }
        private BaseRoomData GetData()
        {
            return DataInitSignals.Instance.onLoadBaseRoomData.Invoke();
        }
        private void SetUpExistingRooms()
        {
            foreach (var room in _data.RoomDatas.Where(roomdata => roomdata.AvailabilityType == AvailabilityType.Unlocked))
            {
                ChangeVisibility(room.BaseRoomType);
            }
        }
        
        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            BaseSignals.Instance.onChangeExtentionVisibility += OnChangeVisibility;
            BaseSignals.Instance.onInformBaseManager += OnUpdateRoomData;
            BaseSignals.Instance.onGetRoomData += OnSetRoomData;
        }
        private void UnsubscribeEvents()
        {
            BaseSignals.Instance.onChangeExtentionVisibility -= OnChangeVisibility;
            BaseSignals.Instance.onInformBaseManager -= OnUpdateRoomData;
            BaseSignals.Instance.onGetRoomData -= OnSetRoomData;
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
        private void OnUpdateRoomData(BaseRoomTypes baseRoomType,RoomData roomData)
        { 
            _data.RoomDatas[(int)baseRoomType] = roomData;
            if (roomData.AvailabilityType == AvailabilityType.Locked)
                return;
            ChangeVisibility(baseRoomType);
        } 
        private RoomData OnSetRoomData(BaseRoomTypes baseRoomType) => _data.RoomDatas[(int) baseRoomType];
        private void ChangeVisibility(BaseRoomTypes baseRoomType)
        {
            baseExtentionController.ChangeExtentionVisibility(baseRoomType);
        }
        [Button]
        private void SaveData()
        {
            DataInitSignals.Instance.onSaveBaseRoomData.Invoke(_data);
        }
    }
}