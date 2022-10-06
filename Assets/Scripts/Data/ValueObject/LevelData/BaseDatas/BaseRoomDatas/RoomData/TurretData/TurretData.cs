using System;
using Enums.Turret;

namespace Data.ValueObject.LevelData
{
    [Serializable]
    public class TurretData
    {
        public AvailabilityType AvailabilityType;
        public bool HasTurretSoldier;
        public int TurretDamage;
        public int TurretCapacity;
        public bool HasSoldier;
        public int SoldierCost;
        public int SoldierPayedAmount;
        public bool IsActive;
    }
}