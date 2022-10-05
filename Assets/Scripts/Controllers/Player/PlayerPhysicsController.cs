using Abstract;
using Controllers.Gate;
using Controllers.Player;
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
                GateEnter(other);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out GatePhysicsController physicsController))
            {
                GateExit(other);
            }
        }
        private void GateEnter(Collider other)
        {
            var playerIsGoingToFrontYard = other.transform.position.z > transform.position.z;
            gameObject.layer = LayerMask.NameToLayer("PlayerBase");
            playerManager.CheckAreaStatus(playerIsGoingToFrontYard ? AreaType.BattleOn : AreaType.BaseDefense);
        }
        private void GateExit(Collider other)
        {
            var playerIsGoingToFrontYard = other.transform.position.z < transform.position.z;
            gameObject.layer = LayerMask.NameToLayer(playerIsGoingToFrontYard ? "PlayerFrontYard" : "PlayerBase");
            playerManager.CheckAreaStatus(playerIsGoingToFrontYard ? AreaType.BattleOn : AreaType.BaseDefense);
            if(!playerIsGoingToFrontYard) return;
            Debug.Log("OnTriggerExit");
            playerManager.DamagableEnemy = null;
            playerManager.HasEnemyTarget = false;
            playerManager.EnemyList.Clear();
        }
    }
}