namespace Interfaces
{
    public interface ICustomer
    {
        bool HasMoney { get; set; }
        void MakePayment(); 
        void StopPayment();
    }
}