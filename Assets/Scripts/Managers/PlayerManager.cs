using System.Collections.Generic;
using Controllers.Player;
using Data.UnityObject;
using Data.ValueObject.PlayerData;
using Data.ValueObject.WeaponData;
using Enums;
using Enums.GameStates;
using Enums.Input;
using Interfaces;
using Keys;
using Signals;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public AreaType CurrentAreaType = AreaType.BaseDefense;
        
        public WeaponTypes WeaponType;
        
        public List<IDamageable> EnemyList = new List<IDamageable>();
        
        public Transform EnemyTarget;
        
        public bool HasEnemyTarget = false;

        #endregion

        #region Serialized Variables

        [SerializeField] 
        private PlayerMeshController meshController;
        [SerializeField] 
        private PlayerAnimationController animationController;
        [SerializeField] 
        private PlayerWeaponController weaponController;
        [SerializeField] 
        private PlayerShootingController shootingController;
        [SerializeField]
        private PlayerMovementController movementController;
        [SerializeField] 
        private PlayerHealthController healthController;
        
        #endregion

        #region Private Variables
        
        private PlayerData _data;

        private WeaponData _weaponData;
        
        #endregion
        
        #endregion
        private void Awake()
        {
            _data = GetPlayerData();
            _weaponData = GetWeaponData();
            Init();
        }
        private PlayerData GetPlayerData() => Resources.Load<CD_Player>("Data/CD_Player").PlayerData;
        private WeaponData GetWeaponData() => Resources.Load<CD_Weapon>("Data/CD_Weapon").WeaponData[(int)WeaponType];
        private void Init() => SetDataToControllers();
        private void SetDataToControllers()
        {
            movementController.SetMovementData(_data.PlayerMovementData);
            weaponController.SetWeaponData(_weaponData);
            meshController.SetWeaponData(_weaponData);
            healthController.SetHealthData(_data);
        }
        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onGetHealthValue += OnGetHealthValue;
            CoreGameSignals.Instance.onTakeDamage += OnTakeDamage;
            InputSignals.Instance.onInputDragged += OnGetInputValues;
            InputSignals.Instance.onInputHandlerChange += OnDisableMovement;
        }
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onGetHealthValue -= OnGetHealthValue;
            CoreGameSignals.Instance.onTakeDamage -= OnTakeDamage;
            InputSignals.Instance.onInputDragged -= OnGetInputValues;
            InputSignals.Instance.onInputHandlerChange -= OnDisableMovement;
        } 
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        private void OnGetInputValues(HorizontalInputParams inputParams)
        {
            movementController.UpdateInputValues(inputParams);
            animationController.PlayAnimation(inputParams);
            AimEnemy();
        }
        public void SetEnemyTarget()
        {
            shootingController.SetEnemyTargetTransform();
            animationController.AimTarget(true);
            AimEnemy();
        }
        private void OnTakeDamage(int damage) => healthController.OnTakeDamage(damage);
        private int OnGetHealthValue() => _data.PlayerHealth;
        public void SetOutDoorHealth() => UISignals.Instance.onOutDoorHealthOpen?.Invoke();
        public void IncreaseHealth() => healthController.IncreaseHealth();
        private void AimEnemy() => movementController.LookAtTarget(!HasEnemyTarget ? null : EnemyList[0]?.GetTransform());
        public void CheckAreaStatus(AreaType areaType) => meshController.ChangeAreaStatus(CurrentAreaType = areaType);
        private void OnDisableMovement(InputHandlers inputHandler) => movementController.DisableMovement(inputHandler);
        public void SetTurretAnim(bool onTurret) => animationController.PlayTurretAnimation(onTurret);
    }
}