using System;
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

        #endregion

        #region Private Variables

        #endregion

        #endregion

        private void OnEnable()
        {
            rigidbody.AddForce(transform.forward*40,ForceMode.VelocityChange);
        }
    }
}