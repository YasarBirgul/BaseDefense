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
        [SerializeField] private Animator animator;
        #endregion

        #region Private Variables
        
        private Vector3 _initialPosition;
        
        private CameraStateTypes _cameraStateType;
        
        private PlayerManager _playerManager;

        #endregion

        #endregion
        
        #region Event Subscriptions
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onReadyToPlay += OnReadyToPlay;
            CoreGameSignals.Instance.onEnterTurret += OnEnterTurret;
            CoreGameSignals.Instance.onLevel += OnLevel;
            CoreGameSignals.Instance.onFinish += OnFinish;
        }
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onReadyToPlay -= OnReadyToPlay;
            CoreGameSignals.Instance.onEnterTurret -= OnEnterTurret;
            CoreGameSignals.Instance.onLevel -= OnLevel;
            CoreGameSignals.Instance.onFinish += OnFinish;
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
        private void OnEnterTurret()
        {
            ChangeCamera(CameraStateTypes.TurretCamera);
           // stateCam.Follow = null;
        }
        private void OnLevel()
        {   
            stateCam.Follow = _playerManager.transform;
            ChangeCamera(CameraStateTypes.GameCamera);
        }
        private void OnFinish()
        {   
            stateCam.Follow = _playerManager.transform;
            ChangeCamera(CameraStateTypes.FinalCamera);
        }
        private void ChangeCamera(CameraStateTypes cameraType)
        {
            animator.Play(cameraType.ToString());
        }
    }
}