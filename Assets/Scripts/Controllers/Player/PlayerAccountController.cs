using System.Threading.Tasks;
using Abstract;
using Interfaces;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerAccountController : MonoBehaviour,ICustomer
    {

        [SerializeField] 
        private MoneyStackerController moneyStackerController;
        
        private bool canPay;

        private int _moneyScore = 10;
        private int _gemAmount;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IStackable>(out IStackable stackable))
            {
                CollectMoney(stackable);
            }
            else if (other.TryGetComponent<Interactable>(out Interactable interactable))
            {
                moneyStackerController.OnRemoveAllStack();
            }
        }
        private void CollectMoney(IStackable stackable)
        {
            moneyStackerController.SetStackHolder(stackable.SendToStack().transform);
            moneyStackerController.GetStack(stackable.SendToStack());
        }
        
        #region Paying Interaction
        public bool CanPay { get => _moneyScore != 0; set { } }
        public async void MakePayment()
        {
            while (true)
            { 
                _moneyScore -= 1;
                Debug.Log(_moneyScore);
                await Task.Delay(100);
                if (CanPay)
                {
                    continue;
                }
                break;
            }
        }
        public void StopPayment()
        {
            canPay = false;
        }
        #endregion
    }
}