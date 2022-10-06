using System;
using System.Collections.Generic;
using Enums;
using UnityEngine.Rendering;

namespace Data.ValueObject.LevelData
{
    [Serializable]
    public class BaseRoomData
    {
        public List<RoomData> RoomDatas=new List<RoomData>();
    }
}