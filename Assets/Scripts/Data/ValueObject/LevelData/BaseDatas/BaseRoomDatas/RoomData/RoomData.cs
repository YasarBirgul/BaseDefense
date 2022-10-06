using System;
using Enums.Turret;

namespace Data.ValueObject.LevelData
{
    [Serializable]
    public class RoomData
    {
        public TurretLocationType TurretLocationType;
        public TurretData TurretData;
        public int RoomCost;
        public int RoomPayedAmount;
    }
}