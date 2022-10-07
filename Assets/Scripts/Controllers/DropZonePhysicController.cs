using System;
using Abstracts;
using Interfaces;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class DropZonePhysicController : MonoBehaviour
    {
        [SerializeField]
        private GemStackerController gemStackerController;
        [SerializeField] private Collider collider;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IStackable>(out IStackable stackable))
            {
            
                if (gemStackerController.PositionList.Count <= gemStackerController.StackList.Count)
                {
                    return;
                }
                gemStackerController.GetStack(stackable.SendToStack(),stackable.SendToStack().transform);
            }
            else if (other.TryGetComponent<Interactable>(out Interactable interactable))
            {
                gemStackerController.OnRemoveAllStack(other.transform);
            }
        }
    }
}