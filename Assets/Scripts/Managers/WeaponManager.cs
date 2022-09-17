using Data.UnityObject;
using Data.ValueObject.WeaponData;
using Enums;
using UnityEngine;

namespace Managers
{
    public class WeaponManager : MonoBehaviour
    {
         #region Self Variables
        
         #region Public Variabl
         #endregion
     
         #region Serialized Variables
         
         [SerializeField] private WeaponTypes weaponType;
         
         #endregion
     
         #region Private Variabl
         private WeaponData _weaponData;
         private Mesh _weaponMesh;
         private Mesh _sideMesh;
         private int _damage;
         private float _attackRate;
         private int _weaponLevel;
         #endregion
         #endregion
         private void Awake()
         {
             _weaponData = GetWeaponData();
             SetData();
         }
         private WeaponData GetWeaponData()
         {
             return Resources.Load<CD_Weapon>("Data/CD_Weapon").WeaponData[(int)weaponType];
         }
         private void SetData()
         {
             _weaponMesh = _weaponData.WeaponMesh;
             _damage = _weaponData.Damage;
             _attackRate = _weaponData.AttackRate;
             _weaponLevel = _weaponData.WeaponLevel;
             if (_weaponData.SideMesh.Count == 0) return;
             _sideMesh = _weaponData.SideMesh[0];
         }
    }
}