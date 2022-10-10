namespace Interfaces
{
    public interface ICustomer
    {
        public bool HasMoney { get; set; }
        void MakePayment();

        void StopPayment();
    }
}