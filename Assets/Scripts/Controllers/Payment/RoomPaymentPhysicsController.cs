using Abstract;
using Interfaces;
using Managers;
using UnityEngine;

namespace Controllers.Payment
{
    public class RoomPaymentPhysicsController : MonoBehaviour
    {
        [SerializeField] private RoomManager roomManager;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ICustomer customer))
            {
                customer.MakePayment();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out ICustomer customer))
            {
                customer.StopPayment();
            }
        }
    }
}