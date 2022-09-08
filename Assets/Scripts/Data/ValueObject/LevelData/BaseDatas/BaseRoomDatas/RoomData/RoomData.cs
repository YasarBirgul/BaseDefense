using System;

namespace Data.ValueObject.LevelData
{
    [Serializable]
    public class RoomData
    { 
        public TurretData TurretData;
        public int RoomCost;
        public int RoomPayedAmount;
    }
}