using Data.ValueObject.LevelData;
using Signals;
using UnityEngine;

namespace Managers
{
    public class BaseRoomManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Private Variables
        
        public BaseRoomData Data;

        #endregion

        #region Serialized Variables

        #endregion

        #endregion
        private void Awake()
        {
            Data = GetData();
        }
        private BaseRoomData GetData()
        {
           return DataInitSignals.Instance.onLoadBaseRoomData.Invoke();
        }
    }
}