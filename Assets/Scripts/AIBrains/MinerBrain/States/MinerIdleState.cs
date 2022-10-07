using Abstract;
using Enums.AI.Miner;
using Managers;

namespace AIBrains.MinerBrain.States
{
    public class MinerIdleState:IState

    {
        private MinerManager _minerManager;
        private MinerAIBrain _minerAIBrain;
        public MinerIdleState(MinerAIBrain minerAIBrain, MinerManager minerManager)
        {
            _minerAIBrain = minerAIBrain;
            _minerManager = minerManager;
        }

        public void Tick()
        {
            
        }

        public void OnEnter()
        {
            _minerAIBrain.SetTargetForMine();
            _minerManager.ChangeAnimation(MinerAnimationStates.Idle);
        }

        public void OnExit()
        {
            
        }
    }
}