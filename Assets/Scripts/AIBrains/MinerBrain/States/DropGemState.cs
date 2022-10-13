using Interfaces;

namespace AIBrains.MinerBrain.States
{
    public class DropGemState:IState
    {
        
        public bool IsGemDropped=>_isGemDropped;
        private bool _isGemDropped;
        private readonly MinerAIBrain _minerAIBrain;
        public DropGemState(MinerAIBrain minerAIBrain)
        {
            _minerAIBrain = minerAIBrain;
        } 
        public void Tick()
        {
        
        }
        public void OnEnter()
        {
            _isGemDropped = true;
        }

        public void OnExit()
        {
            _minerAIBrain.SetTargetForMine();
        }
    }
}