using Managers;
using UnityEngine;

namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
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
            if (other.CompareTag("Gate"))
            {
                gameObject.layer= LayerMask.NameToLayer("Base");
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Gate"))
            {
                gameObject.layer = LayerMask.NameToLayer(other.transform.position.z < transform.position.z 
                    ? "FrontYard"
                    : "Base");
                playerManager.ChangeGameState();
            }
        }
    }
}