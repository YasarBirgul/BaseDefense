using Managers;
using UnityEngine;

namespace Controllers.AI.MoneyWorker
{
    public class MoneyWorkerGenerateButtonPhysics : MonoBehaviour
    {
        [SerializeField] 
        private MoneyWorkerManager manager;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                manager.GenerateSoldier();
            }
        }
    }
}