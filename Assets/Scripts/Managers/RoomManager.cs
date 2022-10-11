using System.Threading.Tasks;
using Controllers.Payment;
using Data.ValueObject.LevelData;
using Enums;
using Enums.Turret;
using Interfaces;
using Signals;
using UnityEngine;

namespace Managers
{
    public class RoomManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        public bool CustomerOnBuyZone=false;
              
        #endregion
        
       
        #region Serialized Variables
        
        [SerializeField] 
        private BaseRoomTypes roomType;
        [SerializeField] 
        private RoomPaymentTextController roomPaymentTextController;

        #endregion
        
        #endregion
        
        #region Private Variables
        
        private RoomData _roomData=new RoomData();

        #endregion
        
        private void OnEnable()
        {
            Init();
        }
        private void Init()
        {
            _roomData = GetRoomData();
            SetUpRoomText(_roomData.RoomCost);
        }
        private RoomData GetRoomData()=>BaseSignals.Instance.onGetRoomData.Invoke(roomType);
        private void SetUpRoomText(int currentRoomCost)=>roomPaymentTextController.SetAndUpdateText(currentRoomCost);
        public async void RoomCostUpdate(int payedAmount, ICustomer customer)
        {
            if(!CustomerOnBuyZone || !customer.HasMoney) return;
            if (_roomData.RoomCost > 0) 
            {
                _roomData.RoomCost -= payedAmount;
                await Task.Delay(100);
                RoomCostUpdate(payedAmount,customer);
            }
            else
            {
                _roomData.AvailabilityType = AvailabilityType.Unlocked;
                CustomerOnBuyZone = false;
                customer.StopPayment();
                InformBaseManager();
            }
            SetUpRoomText(_roomData.RoomCost);
        }
        public void InformBaseManager() =>  BaseSignals.Instance.onInformBaseManager.Invoke(roomType,_roomData);
    }
}