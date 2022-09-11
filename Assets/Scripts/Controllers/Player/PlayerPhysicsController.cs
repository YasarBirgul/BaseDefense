using System;
using Enums.GameStates;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        [SerializeField] private PlayerManager playerManager;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Gate"))
            {
                playerManager.ChangeGameState();
            }
        }
    }
}