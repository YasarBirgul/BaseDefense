using System;
using Abstract;
using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers.Bullet
{
    public class PlayerBulletPhysicsController : MonoBehaviour
    {
        private int _bulletDamage = 20;
        public PlayerShootingController Controller;
        public Rigidbody Rigidbody;
        private void OnEnable()
        {
            Invoke("Disable",0.7f);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageble damagable))
            {
                if(damagable.IsDead)
                    return;
                var health = damagable.TakeDamage(_bulletDamage);
                if (health <= 0)
                {
                    damagable.IsDead = true;
                    Controller.RemoveTarget();
                    Disable();
                }
                else
                {
                    Controller.EnemyTargetStatus();
                }
            }
        } 
        public void ReleaseObject(GameObject obj, PoolType poolName)
        {
            PoolSignals.Instance.onReleaseObjectFromPool?.Invoke(poolName, obj);
        }
        protected void Disable()
        {
            Rigidbody.velocity = Vector3.zero;
            ReleaseObject(gameObject, PoolType.Pistol);
            gameObject.transform.position = Vector3.zero;
        }
    }
}