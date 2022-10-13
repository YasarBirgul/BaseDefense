using System;
using System.Collections.Generic;
using Data.ValueObject.LevelData.FrontYardDatas.FrontYardItemsData;
using Data.ValueObject.LevelData.FrontYardDatas.StageData;

namespace Data.ValueObject.LevelData
{
    [Serializable]
    public class FrontYardData
    {
        public List<StageData> StageDataList;
        public List<FrontYardItemsData> FrontYardItemsDataList;
        public HostageData.HostageData HostageData;
    }
}