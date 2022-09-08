using System;
using UnityEngine;

namespace Data.ValueObject.LevelData
{
    [Serializable]
    public class MilitaryBaseData
    {
        public int MaxSoldierAmount;
        public int CandidateAmount;
        public int CurrentSoldierAmount;
        public int SoldierUpgradeTime;
        public int SoldierSlotCost;
        public int SlotAmount;
        public Transform SlotTransform;
        public int AttackTime;
    }
}