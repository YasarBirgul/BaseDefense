using Abstract;
using Controllers.Gate;
using Enums.GameStates;
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
        #endregion

        #region Private Variables

        #endregion
        
        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out GatePhysicsController physicsController))
            {
                var playerIsGoingToFrontYard = other.transform.position.z > transform.position.z;
                gameObject.layer =  LayerMask.NameToLayer("PlayerBase");
                playerManager.CheckAreaStatus(playerIsGoingToFrontYard ? AreaType.BattleOn : AreaType.BaseDefense);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out GatePhysicsController physicsController))
            {
                var playerIsGoingToFrontYard = other.transform.position.z < transform.position.z;
                gameObject.layer = LayerMask.NameToLayer(playerIsGoingToFrontYard? "PlayerFrontYard" : "PlayerBase");
                playerManager.CheckAreaStatus(playerIsGoingToFrontYard ? AreaType.BattleOn : AreaType.BaseDefense);
            }
        }
    }
}