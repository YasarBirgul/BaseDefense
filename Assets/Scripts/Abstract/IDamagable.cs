using UnityEngine;

namespace Abstract
{
    public interface IDamagable
    { 
        int TakeDamage(int damage);
        Transform GetTransform();
    }
}