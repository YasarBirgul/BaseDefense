using System;
using System.Threading.Tasks;
using Data.ValueObject.PlayerData;
using Enums.GameStates;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerHealthController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables

        [SerializeField]
        private PlayerManager playerManager;

        #endregion

        #region Private Variables

        private PlayerData _data;

        private int _health;

        private const int _increaseAmount = 1;
        
        #endregion

        #endregion

        public void SetHealthData(PlayerData data)
        {
            _data = data;
            Init();
        }
        private void Init()
        {
            _health = _data.PlayerHealth;
        }
        public async void IncreaseHealth()
        {
            if (playerManager.CurrentAreaType != AreaType.BaseDefense)
                return;

            if (_data.PlayerHealth == _health)
            {
                UISignals.Instance.onHealthVisualClose?.Invoke();
                return;
            }
            _health += _increaseAmount;
            CoreGameSignals.Instance.onHealthUpgrade?.Invoke(_health);

            await Task.Delay(50);
            IncreaseHealth();

        }
        public void OnTakeDamage(int damage)
        {
            _health -= damage;
            if (_health <= 0)
            { 
                _health = 0;
                UISignals.Instance.onHealthVisualClose?.Invoke();
                playerManager.ResetPlayer();
            } 
            CoreGameSignals.Instance.onHealthUpgrade?.Invoke(_health);
        }
    }
}