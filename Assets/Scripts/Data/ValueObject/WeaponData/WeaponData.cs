using System;
using Enums.Weapon;

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