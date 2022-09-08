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
            CoreGameSignals.Instance.onPlay += OnPlay;
        }
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        private void OnPlay()
        {
            OnSetCameraTarget();
        }
        private void OnSetCameraTarget()
        {
            if (!_playerManager)
            {
                _playerManager = FindObjectOfType<PlayerManager>();
                stateCam.LookAt = _playerManager.transform;
            }
        }
    }
}