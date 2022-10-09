using System;

namespace Data.ValueObject.LevelData
{
    [Serializable]
    public class LevelData
    {
        public BaseData BaseData;
        public FrontYardData FrontYardData;
        public ScoreData ScoreData;
    }
}