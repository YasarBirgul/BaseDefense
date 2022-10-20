using System.Collections.Generic;
using Controllers.Player;
using Data.UnityObject;
using Data.ValueObject.PlayerData;
using Data.ValueObject.WeaponData;
using DG.Tweening;
using Enums;
using Enums.GameStates;
using Enums.Input;
using Enums.Player;
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

        public IDamageable Damageable;
        
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
        [SerializeField] 
        private PlayerAccountController AccountController;
        [SerializeField] 
        private MoneyStackerController moneyStackerController;
        [SerializeField] 
        private PlayerPhysicsController playerPhysicsController;
        
        #endregion

        #region Private Variables
        
        private PlayerData _data;

        private WeaponData _weaponData;

        private bool _canReset;
        
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
            CoreGameSignals.Instance.onTakePlayerDamage += OnTakeDamage;
            InputSignals.Instance.onInputDragged += OnGetInputValues;
            InputSignals.Instance.onInputHandlerChange += OnDisableMovement;
        }
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onGetHealthValue -= OnGetHealthValue;
            CoreGameSignals.Instance.onTakePlayerDamage -= OnTakeDamage;
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
        }
        public void SetEnemyTarget()
        {
            shootingController.SetEnemyTargetTransform();
            animationController.AimTarget(true);
        }
        public void OnTakeDamage(int damage) => healthController.OnTakeDamage(damage);
        private int OnGetHealthValue() => _data.PlayerHealth;
        public void SetOutDoorHealth() => UISignals.Instance.onOutDoorHealthOpen?.Invoke();
        public void IncreaseHealth() => healthController.IncreaseHealth();
        public void CheckAreaStatus(AreaType areaType) => meshController.ChangeAreaStatus(CurrentAreaType = areaType);
        private void OnDisableMovement(InputHandlers inputHandler) => movementController.DisableMovement(inputHandler);
        public void SetTurretAnim(bool onTurret) => animationController.PlayTurretAnimation(onTurret);
        public void ResetPlayer()
        { 
           AccountController.AccountCollider.enabled = false;
           moneyStackerController.ResetStack();
           CoreGameSignals.Instance.onResetPlayerStack?.Invoke();
           DOVirtual.DelayedCall(.3f,()=>animationController.DeathAnimation());
           playerPhysicsController.ResetPlayerLayer();
           EnemyTarget = null;
           EnemyList.Clear();
           CheckAreaStatus(AreaType.BaseDefense);
           CoreGameSignals.Instance.onReset?.Invoke();
           OnDisableMovement(InputHandlers.None);
           DOVirtual.DelayedCall(3f, () =>
           {
               AccountController.AccountCollider.enabled = true;
               UISignals.Instance.onOutDoorHealthOpen?.Invoke();
               IncreaseHealth();
               _canReset = false;
               transform.position = Vector3.back*3f;
               CoreGameSignals.Instance.onReadyToPlay?.Invoke();
               animationController.ChangeAnimations(PlayerAnimationStates.Idle);
           });
        }
        private void OnPreNextLevel()
        {   
            animationController.ChangeAnimations(PlayerAnimationStates.Idle);
            OnDisableMovement(InputHandlers.None);
            CoreGameSignals.Instance.onReset?.Invoke();
            AccountController.AccountCollider.enabled = false;
            CoreGameSignals.Instance.onHealthUpgrade?.Invoke(_data.PlayerHealth);
            UISignals.Instance.onHealthVisualClose?.Invoke();
            EnemyTarget = null;
            EnemyList.Clear();
            moneyStackerController.ResetStack();
            playerPhysicsController.ResetPlayerLayer();
            CoreGameSignals.Instance.onResetPlayerStack?.Invoke();
            CheckAreaStatus(AreaType.BaseDefense);
            animationController.gameObject.SetActive(false);
        }
        private void OnNextLevel()
        {   
            animationController.gameObject.SetActive(true);
            CoreGameSignals.Instance.onHealthUpgrade?.Invoke(_data.PlayerHealth);
            UISignals.Instance.onHealthVisualClose?.Invoke();
            playerPhysicsController.ResetPlayerLayer();
            EnemyTarget = null;
            EnemyList.Clear();
            CheckAreaStatus(AreaType.BaseDefense);
            transform.position = Vector3.zero;
            animationController.ChangeAnimations(PlayerAnimationStates.Idle);
            AccountController.AccountCollider.enabled = true;
            CoreGameSignals.Instance.onReset?.Invoke();
            CoreGameSignals.Instance.onReadyToPlay?.Invoke();
        }
    }
}