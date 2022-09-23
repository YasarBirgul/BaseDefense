using Abstract;
using UnityEngine;

namespace AIBrains.SoldierBrain
{
    public class Wait : IState
    {
        private Animator _animator;
        public Wait(Animator animator)
        {
            _animator = animator;
        }
        public void Tick()
        {
            
        }
        public void OnEnter()
        {
        }
        public void OnExit()
        {
            
        }
    }
}