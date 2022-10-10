using System.Threading.Tasks;
using Interfaces;
using Signals;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerAccountController : MonoBehaviour,ICustomer
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables
        
        [SerializeField] 
        private MoneyStackerController moneyStackerController;
        
        #endregion

        #region Private Variables
        
        #endregion

        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IStackable>(out IStackable stackable))
            {
                CollectMoney(stackable);
                CoreGameSignals.Instance.onMoneyScoreUpdate.Invoke(+1);
            }
            else if (other.TryGetComponent<IInteractable>(out IInteractable interactable))
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
                await Task.Delay(100);
                if (HasMoney)
                {
                    continue;
                }
                break;
            }
        }
        public void StopPayment()
        {
            HasMoney = false;
        }
        #endregion
    }
}