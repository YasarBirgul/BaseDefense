using Keys;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controllers
{
    public class PlayerMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables,
         
        [SerializeField] private Transform manager;

        #endregion

        #region Private Variables

        #endregion

        #endregion
        public void LookRotation(HorizontalInputParams inputParams)
        {
            var movementDirection = new Vector3(inputParams.MovementVector.x, 0, inputParams.MovementVector.y);
            if (movementDirection == Vector3.zero) return;
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            manager.rotation = Quaternion.RotateTowards( manager.rotation,toRotation,30);
        }
    }
}