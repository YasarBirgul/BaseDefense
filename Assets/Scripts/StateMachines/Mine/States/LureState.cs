﻿using Abstract;
using UnityEngine;

namespace StateMachines.Mine.States
{
    public class LureState : IState
    { 
        private MineBrain _mineBrain;
        private float timer=0;
        public bool IsTimerDone => timer >=_mineBrain.MineManager.LureTime;
        public LureState(MineBrain mineBrain)
        {
            _mineBrain = mineBrain;
        }
        public void UpdateIState()
        {
            timer += Time.deltaTime;
        }
        public void OnEnter()
        {
            ResetTimer();
            _mineBrain.MineManager.LureColliderState(true);
        }
        public void OnExit()
        {
            ResetTimer();
            _mineBrain.MineManager.LureColliderState(false);
        } 
        private void ResetTimer()
        {
            timer = 0;
        }
    }
}