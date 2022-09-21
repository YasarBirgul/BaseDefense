using System;
using Enums;
using UnityEngine;

namespace Data.ValueObject.AIStackData
{
    [Serializable]
    public class AIStackData
    {
        public AIStackType AIStackType;
        public Vector2 Capacity;
        public Vector3 Offset;
    }
}