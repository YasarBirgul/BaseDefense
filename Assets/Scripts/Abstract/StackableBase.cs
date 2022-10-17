using Interfaces;
using UnityEngine;

namespace Abstract
{ 
    public abstract class StackableBase : MonoBehaviour, IStackable
    {
        public virtual bool IsSelected { get; set; }
        public virtual bool IsCollected { get; set; }
        public virtual void SetInit(Transform initTransform, Vector3 position)
        {
            
        }
        public virtual void SetVibration(bool isVibrate)
        {
           
        }
        public virtual void SetSound()
        {
            
        }
        public virtual void EmitParticle()
        {
           
        }
        public virtual void PlayAnimation()
        {
            
        }
        public abstract GameObject SendToStack();
    }
}