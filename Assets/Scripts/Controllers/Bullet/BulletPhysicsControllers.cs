﻿using Abstract;
using Data.ValueObject.WeaponData;
using Interfaces;
using Managers;
using UnityEngine;

namespace Controllers.Bullet
{
    public class BulletPhysicsControllers : MonoBehaviour, IDamager
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] 
        private BulletManager bulletManager;

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
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageble idDamagable))
            {
                bulletManager.SetBulletToPool();
            }
        }
    }
}