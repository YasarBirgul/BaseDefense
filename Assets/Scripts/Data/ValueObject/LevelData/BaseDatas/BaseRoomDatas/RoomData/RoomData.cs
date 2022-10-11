using System;
using Enums;
using Enums.Turret;

namespace Data.ValueObject.LevelData
{
    [Serializable]
    public class RoomData
    {
        public AvailabilityType AvailabilityType;
        public BaseRoomTypes BaseRoomType;
        public TurretLocationType TurretLocationType;
        public TurretData TurretData;
        public int RoomCost;
        public int RoomPayedAmount;
    }
}