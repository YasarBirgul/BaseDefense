using Abstract;
using DG.Tweening;
using Signals;
using UnityEngine;

namespace Concrete
{
    public class StackableBaseMoney : StackableBase
    {
        [SerializeField] 
        private new Rigidbody rigidbody;
        [SerializeField]
        private new BoxCollider collider;
        public override bool IsSelected { get; set; }
        public override bool IsCollected { get; set; }
        private void OnEnable()
        {
            DOVirtual.DelayedCall(0.1f, () => AISignals.Instance.onSetMoneyPosition?.Invoke(this));
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