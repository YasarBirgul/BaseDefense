using Interfaces;
using Managers;
using UnityEngine;

namespace Controllers.Payment
{
    public class RoomPaymentPhysicsController : MonoBehaviour
    {
        [SerializeField] 
        private RoomManager roomManager;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ICustomer customer))
            {
                TakePayment(customer);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out ICustomer customer))
            {
                ExitPayment(customer);
            }
        }
        private void TakePayment(ICustomer customer)
        {
            if (!customer.HasMoney) return;
            customer.MakePayment();
            roomManager.CustomerOnBuyZone = true;
            roomManager.RoomCostUpdate(1, customer);
        }
        private void ExitPayment(ICustomer customer)
        {
            customer.StopPayment();
            roomManager.CustomerOnBuyZone = false;
            roomManager.InformBaseManager();
        }
    }
}