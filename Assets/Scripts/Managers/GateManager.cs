using Controllers.Gate;
using UnityEngine;

namespace Managers
{
    public class GateManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables
        
        [SerializeField]
        private GateMovementController movementController;

        #endregion

        #region Private Variables

        #endregion

        #endregion

        public void GateOpen(bool GateOpenStatus)
        {
            movementController.TurnGateOpen(GateOpenStatus);
        }
    }
}