using Enums;
using Interfaces;
using Signals;
using UnityEngine;

namespace Controllers.Bullet
{ 
    public class BulletFireController : IGetPoolObject
    {
        private WeaponTypes _weaponType;
        public BulletFireController(WeaponTypes weaponType)
        {
            _weaponType = weaponType;
        }
        public GameObject GetObject(PoolType poolName) => PoolSignals.Instance.onGetObjectFromPool.Invoke(poolName);
        public void FireBullets(Transform holderTransform)
        {
            var poolType = (PoolType)System.Enum.Parse(typeof(PoolType),_weaponType.ToString());
            var bullet = GetObject(poolType);
            bullet.transform.position = holderTransform.position;
            bullet.transform.rotation = holderTransform.rotation;
        }
    }
}