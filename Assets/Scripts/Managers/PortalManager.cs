using Controllers.AI.Enemy;
using Signals;
using UnityEngine;

namespace Managers
{
    public class PortalManager : MonoBehaviour
    {
        #region Self variables 

        #region Public Variables
        
        #endregion

        #region Seriliazable Variables

        [SerializeField]
        private PortalController portalController;

        #endregion

        #region Private Variables

        #endregion

        #endregion

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            AISignals.Instance.onOpenPortal += OnOpenPortal;
        }
        private void UnsubscribeEvents()
        {
            AISignals.Instance.onOpenPortal -= OnOpenPortal;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        private void OnOpenPortal()
        {
            portalController.OpenPortal();
        }
    }
}