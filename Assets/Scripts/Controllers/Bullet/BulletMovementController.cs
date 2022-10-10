using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Controllers.Bullet
{
    [RequireComponent(typeof(Rigidbody))]
    public class BulletMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion
        
        #region Serialized Variables

        [SerializeField] 
        private Rigidbody rigidbody;

        private bool bulletHasFired=false;
        #endregion

        #region Private Variables

        #endregion

        #endregion
        private void OnEnable()
        {
            bulletHasFired = true;
        }
        private void FixedUpdate()
        {
            if (!bulletHasFired)
                return;
            FireBullet();
        }
        private async void FireBullet()
        {
            await Task.Delay(10);
            rigidbody.AddRelativeForce(Vector3.forward * 70, ForceMode.VelocityChange);
            bulletHasFired = false;
        }
        private void OnDisable()
        {
            rigidbody.velocity = Vector3.zero;
        }
    }
}