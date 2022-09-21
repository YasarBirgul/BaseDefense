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

        [SerializeField] private GateMeshController meshController;
        [SerializeField] private GatePhysicsController physicsController;

        #endregion

        #region Private Variables

        #endregion

        #endregion

        public void GateOpen(bool GateOpenStatus)
        {
            meshController.TurnGateOpen(GateOpenStatus);
        }
    }
}