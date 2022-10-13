using Abstract;
using Managers;
using UnityEngine;

namespace Controllers.Gate
{
    public class GatePhysicsController : AInteractable
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
            if (other.TryGetComponent(out AInteractable interactable))
            {
                manager.GateOpen(true);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out AInteractable interactable))
            {
                manager.GateOpen(false);
            }
        }
    }
}