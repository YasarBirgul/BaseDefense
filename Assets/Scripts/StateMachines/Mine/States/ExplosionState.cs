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
        public void UpdateIState()
        {
        }

        public void OnEnter()
        {
            Debug.Log("Explode");
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