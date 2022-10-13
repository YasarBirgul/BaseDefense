﻿using Concrete;
using UnityEngine;

namespace Interfaces
{
    public interface IStackable
    {
        void SetInit(Transform initTransform, Vector3 position);
        void SetVibration(bool isVibrate);
        void SetSound();
        void EmitParticle();
        void PlayAnimation();
        GameObject SendToStack();
        void SendStackable(StackableMoney stackableMoney);
        bool IsSelected { get; set; }
        bool IsCollected { get; set; }
    }
}