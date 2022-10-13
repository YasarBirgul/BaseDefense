using Enum;
using Managers;
using UnityEngine;

namespace Controllers.Hostage
{
    public class HostagePhysicsController : MonoBehaviour
    {
        [SerializeField]
        private MinerManager minerManager;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && minerManager._currentType == HostageType.HostageWaiting)
            {
                
                minerManager.AddToHostageStack();
            }
        }
    }
}