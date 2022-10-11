using System;
using Enums.Turret;

namespace Data.ValueObject.LevelData
{
    [Serializable]
    public class MainRoomData
    {
        public TurretLocationType TurretLocationType;
        public TurretData TurretData;
    }
}