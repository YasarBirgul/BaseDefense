using Abstract;
using UnityEngine;

namespace StateMachines.Mine.States
{
    public class ExplosionState : IState
    {
        private MineBrain _mineBrain;
        private float _timer;

        private bool isExplosionHappened=false;
        public bool IsExplosionHappened => isExplosionHappened;
        public ExplosionState(MineBrain mineBrain)
        {
            _mineBrain = mineBrain;
        }
        public void Tick()
        {
            
        }
        public void OnEnter()
        {
            _mineBrain.mineBombManager.ExplosionColliderState(true);
            isExplosionHappened=true;
        }
        public void OnExit()
        {
            _mineBrain.mineBombManager.ExplosionColliderState(false);
            isExplosionHappened=false;
        }
    }
    
}