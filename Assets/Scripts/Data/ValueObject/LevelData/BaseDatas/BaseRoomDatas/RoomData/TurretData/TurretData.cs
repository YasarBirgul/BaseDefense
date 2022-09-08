using System;

namespace Data.ValueObject.LevelData
{
    [Serializable]
    public class TurretData
    {
        public int TurretDamage;
        public int TurretCapacity;
        public bool HasSoldier;
        public int SoldierCost;
        public int SoldierPayedAmount;
        public bool IsActive;
    }
}