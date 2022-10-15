using Controllers.UI;
using Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{ 
    public class UIManager : MonoBehaviour
    {
        #region Self Variables
        
        #region Public Variables
        
        #endregion
        
        #region Serializefield Variables
        
        [SerializeField] 
        private UIPanelController uiPanelController;
        [SerializeField] 
        private TextMeshProUGUI moneyText;
        [SerializeField]
        private TextMeshProUGUI gemText;
        [SerializeField] 
        private TextMeshProUGUI healthText;
        
        [SerializeField] 
        private Scrollbar HealthBar;
      
        #endregion

        #region Private Variables

        private const float _percentage = 100f;
        
        #endregion

        #endregion
        private void Start()
        { 
            OnGetHealthValue();
        }
        #region Event Subscriptions
        private void OnEnable()
        {
            SubscribeEvents();
        } 
        private void SubscribeEvents()
        {
            UISignals.Instance.onOpenPanel += OnOpenPanel;
            UISignals.Instance.onClosePanel += OnClosePanel;
            UISignals.Instance.onSetGemScoreText += OnSetGemScoreText;
            UISignals.Instance.onSetMoneyScoreText += OnSetMoneyScoreText;
            CoreGameSignals.Instance.onHealthUpgrade += OnHealthUpdate;
            UISignals.Instance.onHealthVisualClose += OnHealthFull;
            UISignals.Instance.onOutDoorHealthOpen += OnOutDoorHealthOpen;
            CoreGameSignals.Instance.onReadyToPlay += OnReadyToPlay;
        } 
        private void UnsubscribeEvents()
        {
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;
            UISignals.Instance.onSetGemScoreText -= OnSetGemScoreText;
            UISignals.Instance.onSetMoneyScoreText -= OnSetMoneyScoreText;
            CoreGameSignals.Instance.onHealthUpgrade -= OnHealthUpdate;
            UISignals.Instance.onHealthVisualClose -= OnHealthFull;
            UISignals.Instance.onOutDoorHealthOpen -= OnOutDoorHealthOpen;
            CoreGameSignals.Instance.onReadyToPlay -= OnReadyToPlay;
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
        private void OnSetHealthText(float healthValue) =>  healthText.text = healthValue.ToString();
        private void OnGetHealthValue()=> OnHealthUpdate(CoreGameSignals.Instance.onGetHealthValue.Invoke());
        private void OnOutDoorHealthOpen()
        {
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.HealthBarPanel);
        }
        private void OnHealthFull()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.HealthBarPanel);
        }
        private void OnHealthUpdate(int healthValue)
        {
            OnSetHealthText(healthValue);
            HealthBar.size = healthValue/_percentage;
            if (healthValue == _percentage)
            {
                OnClosePanel(UIPanels.HealthBarPanel);
            }
        }
        public void OnPlayButton()
        {
            CoreGameSignals.Instance.onReadyToPlay.Invoke();
        }
        public void OnNextLevel()
        {
            CoreGameSignals.Instance.onNextLevel.Invoke();
        }
        private void OnReadyToPlay()
        {
            UISignals.Instance.onOpenPanel.Invoke(UIPanels.LevelPanel);
            UISignals.Instance.onClosePanel.Invoke(UIPanels.StartPanel);
        }
    }
}