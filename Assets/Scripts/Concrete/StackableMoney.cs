using Abstract;
using UnityEngine;

namespace Concrete
{
    public class StackableMoney : AStackable
    {
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private BoxCollider collider;
        private void OnEnable()
        {
            SendPosition(transform);
        }
        public override void SetInit(Transform initTransform, Vector3 position)
        {
            base.SetInit(initTransform, position);
        }

        public override void SetVibration(bool isVibrate)
        {
            base.SetVibration(isVibrate);
        }

        public override void SetSound()
        {
            base.SetSound();
        }

        public override void EmitParticle()
        {
            base.EmitParticle();
        }

        public override void PlayAnimation()
        {
            base.PlayAnimation();
        }

        public override GameObject SendToStack()
        {
            rigidbody.useGravity = false;
            rigidbody.isKinematic = true;
            collider.enabled = false;
            return transform.gameObject;
        }
        public override void SendPosition(Transform MoneyPosition)
        {
            base.SendPosition(MoneyPosition);
        }
    }
}