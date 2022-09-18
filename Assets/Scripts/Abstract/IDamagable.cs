using UnityEngine;

namespace Abstract
{
    public interface IDamagable
    {
        int TakeDamage();
        Transform GetTransform();
    }
}