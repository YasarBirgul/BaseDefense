using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Controllers.Bullet;
using Enums;
using Enums.Turret;
using UnityEngine;

namespace Controllers.Turret
{
    public class TurretShootController : MonoBehaviour
    {
        public CapsuleCollider DetectionCollider;
        
        [SerializeField]
        private TurretLocationType turretLocationType;
        [SerializeField] 
        private WeaponTypes weaponType = WeaponTypes.Turret;
        [SerializeField] 
        private List<GameObject> damageables;
        [SerializeField] 
        private Transform weaponHolder;
        public bool readyToAttack { get; set; }
        private BulletFireController fireController;
       
        private const float _fireRate = 1.6f;

        private float maxRadius = 14f;

        private float minRadius = 0f;

        private const float stepRate = 0.5f;

        private const float _tolerance = 0.001f;
        

        private void Awake()
        {
            fireController = new BulletFireController(weaponType);
        }

        public void EnemyInRange(GameObject damageable)
        {
            damageables.Add(damageable);
        }

        public void EnemyOutOfRange(GameObject damageable)
        {
            damageables.Remove(damageable);
            damageables.TrimExcess();
        }
        public void RemoveTarget()
        {
            if (damageables.Count == 0) return;
            damageables.RemoveAt(0);
            damageables.TrimExcess();
        }
        public void ShootTheTarget()
        {
            if (!readyToAttack)
                return;
            
            if (damageables.Count != 0)
            {
                StartCoroutine(FireBullet());
            }
        }
        public async void EnLargeDetectionRadius()
        {
            if (Math.Abs(maxRadius - minRadius) < _tolerance)
            {
                minRadius = 0f;
                return;
            }
            if (minRadius < maxRadius)
            {
                minRadius += stepRate;
                DetectionCollider.radius = minRadius;
            }

            await Task.Delay(100);
            EnLargeDetectionRadius();
        }
        public async void DeSizeDetectionRadius()
        {
            if (Math.Abs(maxRadius - minRadius) < _tolerance)
            {
                maxRadius = 14f;
                return;
            }
            if (maxRadius > minRadius)
            {   
                maxRadius -= stepRate;
                DetectionCollider.radius = maxRadius;
            }
            await Task.Delay(100);
            DeSizeDetectionRadius();
        }
        private IEnumerator FireBullet()
        {
            yield return new WaitForSeconds(_fireRate);
                fireController.FireBullets(weaponHolder);
                ShootTheTarget();
        } 
    }
}