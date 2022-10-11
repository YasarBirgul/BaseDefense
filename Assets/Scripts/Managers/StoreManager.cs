using Data.ValueObject.LevelData;
using Signals;
using UnityEngine;

namespace Managers
{
    public class StoreManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Private Variables

        public BuyablesData Data;

        #endregion

        #region Serialized Variables

        #endregion

        #endregion
        private void Awake()
        {
            Data = GetData();
        }
        private BuyablesData GetData()
        {
            return DataInitSignals.Instance.onLoadBuyablesData.Invoke();
        }
    }
}