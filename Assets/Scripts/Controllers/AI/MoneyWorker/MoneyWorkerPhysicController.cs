using System;
using AIBrains.WorkerBrain.MoneyWorker;
using Controllers.Player;
using Interfaces;
using Signals;
using UnityEngine;

namespace Controllers.AI.MoneyWorker
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
                    stackable.IsCollected = true;
                    AISignals.Instance.onThisMoneyTaken?.Invoke();
                    CoreGameSignals.Instance.onMoneyScoreUpdate.Invoke(1);
                    moneyStackerController.SetStackHolder(stackable.SendToStack().transform);
                    moneyStackerController.GetStack(other.gameObject);
                    moneyWorkerBrain.SetCurrentStock();
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            moneyStackerController.OnRemoveAllStack();
            moneyWorkerBrain.RemoveAllStock();
        }
    } 
}
