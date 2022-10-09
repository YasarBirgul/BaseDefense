namespace Interfaces
{
    public interface ICustomer
    {
        public bool CanPay { get; set; }
        void MakePayment();

        void StopPayment();
    }
}