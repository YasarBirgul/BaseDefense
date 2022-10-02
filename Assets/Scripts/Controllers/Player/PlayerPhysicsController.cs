using Abstract;
using Controllers.Gate;
using Controllers.Player;
using Enums.GameStates;
using Interfaces;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class PlayerPhysicsController : Interactable
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables,
        
        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private MoneyStackerController moneyStackerController;
        [SerializeField] private Collider capsuleCollider;
        #endregion

        #region Private Variables

        #endregion
        
        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out GatePhysicsController physicsController))
            {
                var playerIsGoingToFrontYard = other.transform.position.z > transform.position.z;
                gameObject.layer =  LayerMask.NameToLayer("Base");
                playerManager.CheckAreaStatus(playerIsGoingToFrontYard ? AreaType.BattleOn : AreaType.BaseDefense);
            }
            if (other.TryGetComponent<IStackable>(out IStackable stackable))
            {
                moneyStackerController.SetStackHolder(stackable.SendToStack().transform);
                moneyStackerController.GetStack(stackable.SendToStack());
            }
            else if (other.TryGetComponent<Interactable>(out Interactable interactable))
            {
                moneyStackerController.OnRemoveAllStack();
               // capsuleCollider.enabled = false;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out GatePhysicsController physicsController))
            {
                var playerIsGoingToFrontYard = other.transform.position.z < transform.position.z;
                gameObject.layer = LayerMask.NameToLayer(playerIsGoingToFrontYard? "FrontYard" : "Base");
                playerManager.CheckAreaStatus(playerIsGoingToFrontYard ? AreaType.BattleOn : AreaType.BaseDefense);
            }
        }
    }
}