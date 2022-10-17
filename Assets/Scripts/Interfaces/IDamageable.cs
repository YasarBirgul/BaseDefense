﻿using UnityEngine;

namespace Interfaces
{
    public interface IDamageable
    {
        public bool IsTaken { get; set; }
        public bool IsDead { get; set; }
        Transform GetTransform();
    }
}