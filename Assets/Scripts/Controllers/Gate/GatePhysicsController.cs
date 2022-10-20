using Abstract;
using Controllers.AI.MoneyWorker;
using Managers;
using UnityEngine;

namespace Controllers.Gate
{
    public class GatePhysicsController : InteractableBase
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables,

        [SerializeField] private GateManager manager;
        
        #endregion

        #region Private Variables
        
        #endregion
        
        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out InteractableBase interactable) || other.TryGetComponent(out MoneyWorkerPhysicController moneyWorkerPhysicController))
            {
                manager.GateOpen(true);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out InteractableBase interactable)|| other.TryGetComponent(out MoneyWorkerPhysicController moneyWorkerPhysicController))
            {
                manager.GateOpen(false);
            }
        }
    }
}