using System.Threading.Tasks;
using Controllers.Bullet;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class PlayerShootingController : MonoBehaviour
    {
        [SerializeField] 
        private PlayerManager manager;

        [SerializeField]
        private Transform weaponHolder;

        private BulletFireController fireController;

        private int Speed = 30;
        private void Awake()
        {
            fireController = new BulletFireController(manager.WeaponType,Speed);
        }
        public void SetEnemyTargetTransform()
        {
            manager.EnemyTarget = manager.EnemyList[0].GetTransform();
            manager.DamagableEnemy = manager.EnemyList[0];
            manager.HasEnemyTarget = true;
            Shoot();
        }
        public void EnemyTargetStatus()
        {
            if (manager.EnemyList.Count != 0)
            {
                manager.EnemyTarget = manager.EnemyList[0].GetTransform();
                manager.DamagableEnemy = manager.EnemyList[0];
            }
            else
            {
                manager.HasEnemyTarget = false;
            }
        }
        public void RemoveTarget()
        {
            if (manager.EnemyList.Count == 0) return;
            manager.EnemyList.RemoveAt(0);
            manager.EnemyList.TrimExcess();
            manager.EnemyTarget = null;
            EnemyTargetStatus();
        }
        public async void Shoot()
        {
            if(!manager.EnemyTarget) 
                return;
            if (manager.DamagableEnemy.IsDead)
            {
                RemoveTarget();
            }
            else
            {
                fireController.FireBullets(weaponHolder);
                await Task.Delay(400);
                Shoot();
            }
        }
    }
}