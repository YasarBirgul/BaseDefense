﻿using Abstract;
using Managers;
using UnityEngine;

namespace Controllers.Gate
{
    public class GatePhysicsController : Interactable
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables,

        [SerializeField] private GateManager manager;
        
        #endregion

        #region Private Variables
        
        #endregion
        
        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Interactable interactable))
            {
                manager.GateOpen(true);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Interactable interactable))
            {
                manager.GateOpen(false);
            }
        }
    }
}