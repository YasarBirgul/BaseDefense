using Controllers.Gate;
using Controllers.Turret;
using Enums.GameStates;
using Interfaces;
using Managers;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerPhysicsController : AInteractable
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables,
        
        [SerializeField] 
        private PlayerManager playerManager;
        
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
            if (other.TryGetComponent(out TurretPhysicsController turretPhysicsController))
            {
                playerManager.SetTurretAnim(true);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out GatePhysicsController physicsController))
            {
                GateExit(other);
            }
            if (other.TryGetComponent(out TurretPhysicsController turretPhysicsController))
            {
                playerManager.SetTurretAnim(false);
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
            playerManager.HasEnemyTarget = false;
            playerManager.EnemyList.Clear();
        }
    }
}