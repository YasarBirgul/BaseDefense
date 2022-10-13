using System;

namespace Data.ValueObject.LevelData.FrontYardDatas.StageData
{
    [Serializable]
    public class StageData
    {
        public bool Unlocked;
        public int StageCost;
        public int StagePayedAmount;
    }
}