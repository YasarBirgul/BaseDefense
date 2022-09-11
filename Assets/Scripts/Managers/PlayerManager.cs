using Controllers;
using Data.UnityObject;
using Data.ValueObject.PlayerData;
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

        public GameStates CurrentGameState = GameStates.BaseDefense;
        
        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerMeshController meshController;
        [SerializeField] private PlayerAnimationController animationController;
        
        #endregion

        #region Private Variables
        
        private PlayerData _data;

        private PlayerMovementController _movementController;

        private GameStates _nextState = GameStates.AttackField;
        #endregion
        
        #endregion
        private void Awake()
        {
            _data = GetPlayerData();
            Init();
        }
        private PlayerData GetPlayerData() => Resources.Load<CD_Player>("Data/CD_Player").PlayerData;
        private void Init()
        {
            _movementController = GetComponent<PlayerMovementController>();
            SetDataToControllers();
        }
        private void SetDataToControllers()
        {
            _movementController.SetMovementData(_data.PlayerMovementData);
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
            meshController.LookRotation(inputParams);
            animationController.PlayAnimation(inputParams);
        }

        public void ChangeGameState()
        {
            if (CurrentGameState != _nextState)
            {
                CurrentGameState = _nextState;
            }
            else
            {
                CurrentGameState = GameStates.BaseDefense;
            }
            Debug.Log(CurrentGameState);
        }
    }
}