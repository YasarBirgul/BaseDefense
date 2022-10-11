using System;

namespace Data.ValueObject.LevelData
{
    [Serializable]
    public class MilitaryBaseData
    {
        public int BaseCapacity;
        public int TotalAmount;
        public int TentCapacity;
        public int CurrentSoldierAmount;
        public int SoldierUpgradeTime;
        public int SoldierSlotCost;
        public int SlotAmount;
        public int AttackTime;
    }
}