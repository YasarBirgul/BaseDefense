using Enums;
using Interfaces;
using Signals;
using UnityEngine;

namespace Controllers.Bullet
{
    public class BulletFireController : IGetPoolObject
    {
        private Transform _holderTransform;
        private WeaponTypes _weaponType;
        private Rigidbody _rigidbody;
        private float _speed;

        public BulletFireController(Transform holderHolderTransform,WeaponTypes weaponType,Rigidbody rigidbody,float speed)
        {
            _holderTransform = holderHolderTransform;
            _weaponType = weaponType;
            _rigidbody = rigidbody;
            _speed = speed;
        }
        public GameObject GetObject(PoolType poolName)
        {
            return PoolSignals.Instance.onGetObjectFromPool.Invoke(poolName);
        }
        public GameObject FireBullets()
        {
            var poolType = (PoolType)System.Enum.Parse(typeof(PoolType),_weaponType.ToString());
            var bullet = GetObject(poolType);
            bullet.transform.position = _holderTransform.position;
            bullet.transform.rotation = _holderTransform.transform.rotation;
            _rigidbody.AddForce(_holderTransform.transform.forward*_speed,ForceMode.VelocityChange);
            return bullet;
        }
    }
}