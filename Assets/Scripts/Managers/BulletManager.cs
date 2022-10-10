using System;
using Controllers.Bullet;
using Data.UnityObject;
using Data.ValueObject.WeaponData;
using Enums;
using Interfaces;
using Signals;
using UnityEngine;

namespace Managers
{
    public class BulletManager: MonoBehaviour, IReleasePoolObject
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private WeaponTypes weaponType;
        [SerializeField] private BulletPhysicsControllers physicsController;
        
        #endregion

        #region Private Variables

        private WeaponData _data;

        #endregion
        
        #endregion
        private void Awake()
        {
            _data = GetBulletData();
            SetDataToControllers();
        } 
        private WeaponData GetBulletData() => Resources.Load<CD_Weapon>("Data/CD_Weapon").WeaponData[(int)weaponType];
        private void SetDataToControllers()
        {
            physicsController.GetData(_data);
        }
        public void ReleaseObject(GameObject obj, PoolType poolName)
        {
            PoolSignals.Instance.onReleaseObjectFromPool.Invoke(poolName,obj);
        }
        public void SetBulletToPool()
        {
            var poolName = (PoolType)System.Enum.Parse(typeof(PoolType),weaponType.ToString());
            ReleaseObject(gameObject,poolName);
        }
    }
}