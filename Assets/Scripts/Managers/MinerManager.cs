using AIBrains.MinerBrain;
using Enum;
using Enums.AI.Miner;
using Signals;
using UnityEngine;

namespace Managers
{
    public class MinerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public MinerAIBrain minerAIBrain { get; private set; }
        
        #endregion

        #region Serialized Variables
        
        
        [SerializeField]
        private Animator animator;


        #endregion

        #region Private Variables

        public HostageType _currentType=HostageType.HostageWaiting; 
        private int _currentLevel; 
        private Transform _currentMinePlace;


        #endregion

        #endregion
        private void Awake()
        {
            //_currentMinePlace=mineBaseManager.GetRandomMineTarget();
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            DropzoneSignals.Instance.onDropZoneFull += OnDropZoneFull;
        }

        private void OnDropZoneFull(bool _state)
        {
            minerAIBrain.IsDropZoneFullStatus=_state;
        }
        
        private void UnSubscribeEvents()
        {
            DropzoneSignals.Instance.onDropZoneFull -= OnDropZoneFull;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        #endregion

        public void ChangeAnimation(MinerAnimationStates minerAnimationStates)
        {
            animator.SetTrigger(minerAnimationStates.ToString());
        }

        public void AddToHostageStack()
        {
            _currentType = HostageType.Hostage;
            HostageSignals.Instance.onAddHostageStack?.Invoke(this);
        }
    }
}