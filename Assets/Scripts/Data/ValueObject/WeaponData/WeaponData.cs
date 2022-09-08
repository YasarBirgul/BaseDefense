using System;
using Enums;

namespace Data.ValueObject.WeaponData
{
    [Serializable]
    public class WeaponData
    {
        public WeaponTypes WeaponType;
        public int Damage;
        public float AttackRate;
    }
}