using System;

namespace Data.ValueObject.LevelData
{
    [Serializable]
    public class MineBaseData
    {
        public int MaxWorkerAmount;
        public int CurrentWorkerAmount;
        public int GemCollectionOffset;
    }
}