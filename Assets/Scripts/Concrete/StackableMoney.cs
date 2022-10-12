using Abstract;
using UnityEngine;

namespace Concrete
{
    public class StackableMoney : AStackable
    {
        [SerializeField] 
        private new Rigidbody rigidbody;
        [SerializeField]
        private new BoxCollider collider;
        private void OnEnable()
        {
            SendPosition(transform);
            collider.enabled = true;
            rigidbody.isKinematic = false;
            rigidbody.useGravity = true;
        }
        public override GameObject SendToStack()
        {
            rigidbody.useGravity = false;
            rigidbody.isKinematic = true;
            collider.enabled = false;
            return transform.gameObject;
        }
    }
}