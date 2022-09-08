using System;

namespace Data.ValueObject.LevelData
{
    [Serializable]
    public class StageData
    {
        public bool Unlocked;
        public int StageCost;
        public int StagePayedAmount;
    }
}