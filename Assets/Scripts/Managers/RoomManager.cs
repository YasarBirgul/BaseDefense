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
        public bool RoomBought=false;
        
        [SerializeField] 
        private BaseRoomTypes roomType;
        [SerializeField] 
        private RoomPaymentTextController roomPaymentTextController;
        
        private RoomData _roomData=new RoomData();
        private void OnEnable()
        {
            _roomData=GetRoomData();
            SetUpRoomText(_roomData.RoomCost);
        }
        private RoomData GetRoomData()=>BaseSignals.Instance.onGetRoomData.Invoke(roomType);
        private void SetUpRoomText(int currentRoomCost)=>roomPaymentTextController.SetAndUpdateText(currentRoomCost);
        public async void RoomCostUpdate(int payedAmount, ICustomer customer)
        {
            if(RoomBought || !customer.CanPay) return;
            
            _roomData.RoomCost -= payedAmount;
            SetUpRoomText(_roomData.RoomCost);
            
            await Task.Delay(100);
            
            if (_roomData.RoomCost != 0) 
            {
                RoomCostUpdate(payedAmount,customer);
            }
            else
            {
                _roomData.AvailabilityType = AvailabilityType.Unlocked;
                RoomBought = true;
                InformBaseManager();
            }
        }
        public void InformBaseManager() =>  BaseSignals.Instance.onInformBaseRoom.Invoke(roomType,_roomData);
    }
}