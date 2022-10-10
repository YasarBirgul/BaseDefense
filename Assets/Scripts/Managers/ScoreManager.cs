using Data;
using Signals;
using UnityEngine;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        private ScoreData _scoreData;
        
        #region Event Subscriptions
        private void OnEnable()
        {
            SubscribeEvents();
        } 
        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize += OnLevelInitialize;
            CoreGameSignals.Instance.onMoneyScoreUpdate += OnMoneyScoreUpdate;
            CoreGameSignals.Instance.onDiamondScoreUpdate += OnDiamondScoreUpdate;
            CoreGameSignals.Instance.onHasEnoughMoney += OnHasEnoughMoney;
            CoreGameSignals.Instance.onApplicationQuit += OnApplicationQuit;
        } 
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize -= OnLevelInitialize;
            CoreGameSignals.Instance.onMoneyScoreUpdate -= OnMoneyScoreUpdate;
            CoreGameSignals.Instance.onDiamondScoreUpdate -= OnDiamondScoreUpdate;
            CoreGameSignals.Instance.onHasEnoughMoney -= OnHasEnoughMoney;
            CoreGameSignals.Instance.onApplicationQuit -= OnApplicationQuit;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        private void OnLevelInitialize()
        {
            _scoreData = GetScoreData();
            UpdateGemScoreText(_scoreData.DiamondScore);
            UpdateMoneyScoreText(_scoreData.MoneyScore);
        }
        private ScoreData GetScoreData() => DataInitSignals.Instance.onLoadScoreData.Invoke();
        private void UpdateGemScoreText(int diamondScore) => UISignals.Instance.onSetGemScoreText?.Invoke(diamondScore);
        private void UpdateMoneyScoreText(int moneyScore) => UISignals.Instance.onSetMoneyScoreText?.Invoke(moneyScore);
        private void OnMoneyScoreUpdate(int moneyScore) => UpdateMoneyScoreText(_scoreData.MoneyScore += moneyScore);
        private void OnDiamondScoreUpdate(int gemScore) => UpdateGemScoreText(_scoreData.DiamondScore += gemScore);
        private bool OnHasEnoughMoney() =>_scoreData.MoneyScore != 0;
        private void UpdateGameScoreData() => DataInitSignals.Instance.onSaveScoreData.Invoke(_scoreData);
        private void OnApplicationQuit() => UpdateGameScoreData();
    }
}