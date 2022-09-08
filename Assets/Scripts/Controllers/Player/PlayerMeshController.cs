using Keys;
using UnityEngine;

namespace Controllers
{
    public class PlayerMeshController : MonoBehaviour
    {
        public void LookRotation(HorizontalInputParams inputParams)
        {
            Vector3 movementDirection = new Vector3(inputParams.InputVector.x, 0, inputParams.InputVector.y);
            if (movementDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards( transform.rotation,toRotation,30);
            }
        }
    }
}