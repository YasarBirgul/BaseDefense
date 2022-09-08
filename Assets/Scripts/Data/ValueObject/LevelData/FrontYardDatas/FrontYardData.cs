using System;
using System.Collections.Generic;

namespace Data.ValueObject.LevelData
{
    [Serializable]
    public class FrontYardData
    {
        public List<StageData> StageDataList;
        public List<FrontYardItemsData> FrontYardItemsDataList;
    }
}