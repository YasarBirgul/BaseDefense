using System.Collections.Generic;
using System.Threading.Tasks;
using Abstract;
using Controllers;
using Data.UnityObject;
using Data.ValueObject.PlayerData;
using Data.ValueObject.WeaponData;
using Enums;
using Enums.GameStates;
using Keys;
using Signals;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public AreaType currentAreaType = AreaType.BaseDefense;
        public WeaponTypes WeaponType;
        
        public List<IDamagable> EnemyList = new List<IDamagable>();
        public Transform EnemyTarget;
        public bool HasEnemyTarget = false;

        public IDamagable DamagableEnemy;
        
        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerMeshController meshController;
        [SerializeField] private PlayerAnimationController animationController;
        [SerializeField] private PlayerWeaponController weaponController;
        [SerializeField] private PlayerShootingController shootingController;
        [SerializeField] private PlayerMovementController movementController;
        #endregion

        #region Private Variables
        
        private PlayerData _data;

        private WeaponData _weaponData;

        private AreaType _nextState = AreaType.BattleOn;
        
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
        private void Init()
        {
            SetDataToControllers();
        }
        private void SetDataToControllers()
        {
            movementController.SetMovementData(_data.PlayerMovementData);
            weaponController.SetWeaponData(_weaponData);
            meshController.SetWeaponData(_weaponData);
        }
        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputDragged += OnGetInputValues;
        }
        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputDragged -= OnGetInputValues;
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
            if (!HasEnemyTarget) return;
            AimEnemy();
        }
        public void CheckAreaStatus(AreaType AreaStatus)
        {
            currentAreaType = AreaStatus;
            meshController.ChangeAreaStatus(AreaStatus);
        }
        public void SetEnemyTarget()
        {
            shootingController.SetEnemyTargetTransform();
            animationController.AimTarget(true);
            AimEnemy();
        }
        private void AimEnemy()
        { 
            if (EnemyList.Count != 0)
            {
                var transformEnemy = EnemyList[0].GetTransform();
                movementController.RotateThePlayer(transformEnemy);
            }
        }
    }
}