﻿using Interfaces;
using UnityEngine;

namespace Abstract
{
    public abstract class AStackable : MonoBehaviour, IStackable
    {
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