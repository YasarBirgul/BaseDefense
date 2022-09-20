using Abstract;
using UnityEngine;

namespace AIBrains.SoldierBrain
{
    public class Wait : IState
    {
        public Wait()
        {
            
        }
        public void Tick()
        {
            
        }
        public void OnEnter()
        {
            Debug.Log("EnterWait");
        }

        public void OnExit()
        {
            
        }
    }
}