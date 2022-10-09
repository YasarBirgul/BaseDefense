using Enums;
using Interfaces;
using Signals;
using UnityEngine;

namespace Controllers.Bullet
{
    public class BulletFireController : IGetPoolObject
    {
        private WeaponTypes _weaponType;
        private float _speed;
        
        public BulletFireController(WeaponTypes weaponType,float speed)
        {
            _weaponType = weaponType;
            _speed = speed;
        }
        public GameObject GetObject(PoolType poolName)
        {
            return PoolSignals.Instance.onGetObjectFromPool.Invoke(poolName);
        }
        public GameObject FireBullets(Transform holderTransform)
        {
            var poolType = (PoolType)System.Enum.Parse(typeof(PoolType),_weaponType.ToString());
            var bullet = GetObject(poolType);
            bullet.transform.position = holderTransform.position;
            bullet.transform.rotation = Quaternion.LookRotation(holderTransform.rotation.eulerAngles);
            return bullet;
        }
    }
}