using Controllers.Base;
using Data.UnityObject;
using Data.ValueObject.LevelData;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class BaseManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
       
        [SerializeField] private BaseRoomExtentionController extentionController;
        
        #endregion
    
        #region Public Variables

        #endregion

        #region Private Variables

        private LevelData _levelData;
        
        #endregion
        
        #endregion
        
        #region Event Subscription
        private void Awake()
        {
            _levelData = GetLevelData();
        }
        private LevelData GetLevelData() => Resources.Load<CD_Level>("BaseDefense/CD_Level").LevelDatas[0];

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
            extentionController.ChangeExtentionVisibility(baseRoomType);
        }
    }
}