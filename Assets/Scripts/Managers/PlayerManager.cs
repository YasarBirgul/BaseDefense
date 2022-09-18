using Controllers;
using Data.UnityObject;
using Data.ValueObject.PlayerData;
using Data.ValueObject.WeaponData;
using Enums;
using Enums.GameStates;
using Keys;
using Signals;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [FormerlySerializedAs("CurrentGameState")] public AreaType currentAreaType = AreaType.BaseDefense;
        public WeaponTypes WeaponType;
        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerMeshController meshController;
        [SerializeField] private PlayerAnimationController animationController;
        [SerializeField] private PlayerWeaponController weaponController;

        #endregion

        #region Private Variables
        
        private PlayerData _data;

        private WeaponData _weaponData;

        private PlayerMovementController _movementController;

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
            _movementController = GetComponent<PlayerMovementController>();
            SetDataToControllers();
        }
        private void SetDataToControllers()
        {
            _movementController.SetMovementData(_data.PlayerMovementData);
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
            _movementController.UpdateInputValues(inputParams);
            animationController.PlayAnimation(inputParams);
        }
        public void CheckAreaStatus(AreaType AreaStatus)
        {
            currentAreaType = AreaStatus;
            meshController.ChangeAreaStatus(AreaStatus);
        }
    }
}