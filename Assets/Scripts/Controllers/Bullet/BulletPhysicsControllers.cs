using Data.ValueObject.WeaponData;
using Interfaces;
using UnityEngine;

namespace Controllers.Bullet
{
    public class BulletPhysicsControllers : MonoBehaviour, IDamager
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        #endregion

        #region Private Variables
        
        private int _damage;
        
        #endregion
        
        #endregion

        public void GetData(WeaponData data)
        {
            _damage = data.Damage;
        }
        public int Damage()
        {
            return _damage;
        }
    }
}