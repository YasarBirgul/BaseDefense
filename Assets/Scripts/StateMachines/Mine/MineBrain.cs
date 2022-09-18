using System;
using Abstract;
using Managers;
using StateBehaviour;
using StateMachines.Mine.States;
using UnityEngine;
using UnityEngine.Serialization;

namespace StateMachines.Mine
{
    public class MineBrain : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [FormerlySerializedAs("MineManager")] public MineBombManager mineBombManager;

        #endregion

        #region Serialized Variables,
        
        #endregion

        #region Private Variables

        private StateMachine _stateMachine;

        #endregion

        #endregion
        private void Awake()
        {
            GetStatesReferences();
        }
        private void GetStatesReferences()
        {
            var _readyState = new ReadyState();
            var _lureState = new LureState(this);
            var _explosionState =new ExplosionState(this);
            var _mineCountDownState =new MineCountDownState(this);
            _stateMachine = new StateMachine();
            At(_readyState,_lureState,()=>mineBombManager.IsPayedTotalAmount);
            At(_lureState,_explosionState,()=>_lureState.IsTimerDone);
            At(_explosionState,_mineCountDownState,()=>_explosionState.IsExplosionHappened);
            At(_mineCountDownState,_readyState,()=>_mineCountDownState.IsTimerDone);
            _stateMachine.SetState(_readyState);
            void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
        }
        private void Update() => _stateMachine.UpdateIState();
    }
}