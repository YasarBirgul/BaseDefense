using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Data.ValueObject.WeaponData
{
    [Serializable]
    public class WeaponData
    {
        public WeaponTypes WeaponType;
        public Mesh WeaponMesh;
        public List<Mesh> SideMesh;
        public int Damage;
        public float AttackRate;
        public int WeaponLevel=1;
    }
}