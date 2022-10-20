using Abstract;
using Concrete;
using Interfaces;
using UnityEngine;

namespace Controllers.GemArea
{
    public class DropZonePhysicController : MonoBehaviour
    {
        [SerializeField]
        private GemStackerController gemStackerController;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<StackableBaseGem>(out StackableBaseGem stackable))
            {
                if (gemStackerController.PositionList.Count <= gemStackerController.StackList.Count)
                {
                    return;
                }
                gemStackerController.GetStack(stackable.SendToStack(),stackable.SendToStack().transform);
            }
            else if (other.TryGetComponent<InteractableBase>(out InteractableBase interactable))
            {
                gemStackerController.OnRemoveAllStack(other.transform);
            }
        }
    }
}