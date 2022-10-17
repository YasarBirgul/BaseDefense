using UnityEngine;

namespace Interfaces
{
    public interface IDamageable
    { 
        bool IsTaken { get; set; }
        bool IsDead { get; set; }
        Transform GetTransform();
    }
}