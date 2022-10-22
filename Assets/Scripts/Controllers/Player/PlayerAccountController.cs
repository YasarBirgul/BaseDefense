using System.Threading.Tasks;
using Abstract;
using Concrete;
using Interfaces;
using Signals;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerAccountController : MonoBehaviour,ICustomer
    {
        #region Self Variables

        #region Public Variables

        public Collider AccountCollider;
        
        #endregion

        #region Serialized Variables
        
        [SerializeField] 
        private MoneyStackerController moneyStackerController;

        #endregion

        #region Private Variables

        private bool _canPay=true;
        
        #endregion

        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<StackableBaseMoney>(out StackableBaseMoney stackable))
            {
                CollectMoney(stackable);
                CoreGameSignals.Instance.onMoneyScoreUpdate.Invoke(+1);
            }
            if (other.TryGetComponent<StackableBaseGem>(out StackableBaseGem stackableGem))
            {
                
            }
            else if (other.TryGetComponent<InteractableBase>(out InteractableBase interactable))
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
        public bool HasMoney { get => CoreGameSignals.Instance.onHasEnoughMoney.Invoke(); set { } }
        
        public async void MakePayment()
        {
            while (true)
            {
                CoreGameSignals.Instance.onMoneyScoreUpdate.Invoke(-1);
                if (HasMoney && _canPay)
                {
                    await Task.Delay(100);
                    continue;
                }
                break;
            }
        }
        
        public async void StopPayment()
        {
            _canPay = false;
            await Task.Delay(200);
            _canPay = true;
        }
        public void PaymentStackAnimation(Transform transform)
        {
            moneyStackerController.PaymentStackAnimation(transform);
        }

        #endregion
    }
}