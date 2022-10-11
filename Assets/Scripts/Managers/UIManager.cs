using Controllers.UI;
using TMPro;
using UnityEngine;

namespace Managers
{ 
    public class UIManager : MonoBehaviour
    {
        #region Self Variables
        #region Public Vars
        #endregion
        #region Serializefield Variables
        
        [SerializeField] 
        private UIPanelController uiPanelController;
        [SerializeField] 
        private TextMeshProUGUI moneyText;
        [SerializeField]
        private TextMeshProUGUI gemText;

        #endregion

        #region Private Vars

        #endregion

        #endregion

        private void Awake()
        {
            uiPanelController = new UIPanelController();
        }
        
        #region Event Subscriptions
        private void OnEnable()
        {
            SubscribeEvents();
        } 
        private void SubscribeEvents()
        {
          //  CoreGameSignals.Instance.onGameOpen += OnGameOpen;
            UISignals.Instance.onOpenPanel += OnOpenPanel;
            UISignals.Instance.onClosePanel += OnClosePanel;
            UISignals.Instance.onSetGemScoreText += OnSetGemScoreText;
            UISignals.Instance.onSetMoneyScoreText += OnSetMoneyScoreText;
        } 
        private void UnsubscribeEvents()
        {
         //   CoreGameSignals.Instance.onGameOpen += OnGameOpen;
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;
            UISignals.Instance.onSetGemScoreText -= OnSetGemScoreText;
            UISignals.Instance.onSetMoneyScoreText -= OnSetMoneyScoreText;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        private void OnOpenPanel(UIPanels panel) => uiPanelController.OpenPanel(panel);
        private void OnClosePanel(UIPanels panel) => uiPanelController.ClosePanel(panel);
        private void OnSetGemScoreText(int gemScore) =>   gemText.text = gemScore.ToString();
        private void OnSetMoneyScoreText(int moneyScore) =>moneyText.text = moneyScore.ToString();
        public void PlayButton()
        {
            uiPanelController.OpenPanel(UIPanels.levelPanel);
            uiPanelController.ClosePanel(UIPanels.PlayPanel);
        }
    }
}