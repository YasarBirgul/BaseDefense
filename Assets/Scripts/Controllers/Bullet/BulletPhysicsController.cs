using Abstract;
using UnityEngine;

namespace Controllers.Bullet
{
    public class BulletPhysicsController : MonoBehaviour 
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamagable damagable))
            {
                
            }
        }
    }
}