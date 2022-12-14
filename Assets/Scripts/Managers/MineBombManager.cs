using System;
using Abstract;
using Controllers;
using Controllers.Mine;
using Interfaces;
using UnityEngine;

namespace Managers
{
    public class MineBombManager : MonoBehaviour, IDamageable
    {
        #region Self Variables

        #region Public Variables

        public bool IsPayedTotalAmount => (_payedGemAmount > _requiredGemAmount);
        public int GemAmount;
        public int LureTime;
        public int MineCountDownTime;
        public int ExplosionDamage;
        
        #endregion

        #region Serialized Variables,

        [SerializeField] private MinePhysicsController minePhysicsController;
        [SerializeField] private int requiredGemAmount;

        #endregion

        #region Private Variables

        private int _payedGemAmount=0;
        private int _requiredGemAmount;

        #endregion
        
        #endregion
        private void Awake()
        {
         
        }
        
        public void ShowGemAmountText()
        {

        }

        public void LureColliderState(bool _state)
        {
            minePhysicsController.LureColliderState(_state);
        }

        public void ExplosionColliderState(bool _state)
        {
            minePhysicsController.ExplosionColliderState(_state);
        }

        public void PayGemToMine()
        {
            GemAmount--;
            _payedGemAmount++;

        }
        public int TakeDamage()
        {
            return ExplosionDamage;
        }

        public bool IsTaken { get; set; }
        public bool IsDead { get; set; }

        public int TakeDamage(int damage)
        {
            throw new NotImplementedException();
        }
        public Transform GetTransform()
        {
            throw new NotImplementedException();
        }
    }
}