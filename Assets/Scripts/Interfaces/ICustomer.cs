using UnityEngine;

namespace Interfaces
{
    public interface ICustomer
    {
        bool HasMoney { get; set; }
        void MakePayment(); 
        void StopPayment();
        void PaymentStackAnimation(Transform transform);
    }
}