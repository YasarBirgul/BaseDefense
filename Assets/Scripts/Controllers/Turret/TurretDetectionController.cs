﻿using Abstract;
using Interfaces;
using UnityEngine;

namespace Controllers.Turret
{
    public class TurretDetectionController : MonoBehaviour
    {
        [SerializeField] 
        private TurretShootController shootController;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out IDamageable damageable)) return;
            if (!damageable.IsTaken)
                shootController.EnemyInRange(damageable.GetTransform().gameObject);
            shootController.ShootTheTarget();
        }
        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out IDamageable damageable)) return;
            shootController.EnemyOutOfRange(damageable.GetTransform().gameObject);
            damageable.IsTaken = false;
        }
    }
}