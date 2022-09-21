using Cinemachine;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables
        
        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private CinemachineStateDrivenCamera stateCam;
        
        #endregion

        #region Private Variables
        
        private Vector3 _initialPosition;
        
        private  Animator _animator;

        private CameraStateTypes _cameraStateType;
        
        private PlayerManager _playerManager;

        #endregion

        #endregion
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        #region Event Subscriptions
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onReadyToPlay += OnReadyToPlay;
        }
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onReadyToPlay -= OnReadyToPlay;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        private void OnReadyToPlay()
        {
            OnSetCameraTarget();
        }
        private void OnSetCameraTarget()
        {
            if(!_playerManager)
                _playerManager = FindObjectOfType<PlayerManager>();
            stateCam.Follow = _playerManager.transform;
        }
    }
}