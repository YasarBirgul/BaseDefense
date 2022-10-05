using System.Collections.Generic;
using Controllers.Player;
using UnityEngine;
using StateMachines.AIBrain.Workers;
using Signals;
using Interfaces;

namespace Controllers
{
    public class MoneyWorkerPhysicController : MonoBehaviour
    {
        [SerializeField]
        private MoneyWorkerAIBrain moneyWorkerBrain;
        [SerializeField]
        private MoneyStackerController moneyStackerController;

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<IStackable>(out IStackable stackable))
            {
                if (moneyWorkerBrain.IsAvailable())
                {
                    AISignals.Instance.onThisMoneyTaken?.Invoke(other.transform);
                    moneyStackerController.SetStackHolder(stackable.SendToStack().transform);
                    moneyStackerController.GetStack(other.gameObject);
                    moneyWorkerBrain.SetCurrentStock();
                    //other'a layer de�i�tirme yap�labilir
                }
            }
            if (other.CompareTag("Gate"))
            {
                Debug.Log("Zort Gate");
                moneyStackerController.OnRemoveAllStack();
                moneyWorkerBrain.RemoveAllStock();
            }
        }
    } 
}
