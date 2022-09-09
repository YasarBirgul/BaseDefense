using Keys;
using UnityEngine;

namespace Controllers
{
    public class PlayerMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables,
         
        [SerializeField] private Transform managerTransform;

        #endregion

        #region Private Variables
        
        #endregion

        #endregion
        public void LookRotation(HorizontalInputParams inputParams)
        {
            Vector3 movementDirection = new Vector3(inputParams.MovementVector.x, 0, inputParams.MovementVector.y);
            if (movementDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                managerTransform.rotation = Quaternion.RotateTowards( managerTransform.rotation,toRotation,30);
            }
        }
    }
}