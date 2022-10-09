using System;
using Controllers.Payment;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class RoomManager : MonoBehaviour
    {
        [SerializeField] private BaseRoomTypes roomType;

        [SerializeField] private RoomPaymentPhysicsController roomPaymentPhysicsController;
        [SerializeField] private RoomPaymentTextController roomPaymentTextController;
        
        
        public void TakePayment()
        {
            
        }
    }
}