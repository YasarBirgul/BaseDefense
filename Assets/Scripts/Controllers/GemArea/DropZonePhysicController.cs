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
            if (other.TryGetComponent<StackableGem>(out StackableGem stackable))
            {
                if (gemStackerController.PositionList.Count <= gemStackerController.StackList.Count)
                {
                    return;
                }
                gemStackerController.GetStack(stackable.SendToStack(),stackable.SendToStack().transform);
            }
            else if (other.TryGetComponent<AInteractable>(out AInteractable interactable))
            {
                gemStackerController.OnRemoveAllStack(other.transform);
            }
        }
    }
}