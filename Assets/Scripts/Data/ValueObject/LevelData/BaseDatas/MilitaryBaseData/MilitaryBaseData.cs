using System;
using UnityEngine;

namespace Data.ValueObject.LevelData
{
    [Serializable]
    public class MilitaryBaseData
    {
        public Vector2 SlotsGrid;
        public Vector2 SlotOffSet;
        public GameObject SlotPrefab;
        public int BaseCapacity;
        public int TotalAmount;
        public int TentCapacity;
        public int CurrentSoldierAmount;
        public int SoldierUpgradeTime;
        public int SoldierSlotCost;
        public int SlotAmount;
        public Transform SlotTransform;
        public int AttackTime;
        public Transform frontYardSoldierPosition;
    }
}