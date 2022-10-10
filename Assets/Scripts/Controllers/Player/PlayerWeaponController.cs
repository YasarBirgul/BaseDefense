using Data.ValueObject.WeaponData;
using UnityEngine;

namespace Managers
{
    public class PlayerWeaponController : MonoBehaviour
    {
         #region Self Variables
        
         #region Public Variables
         
         #endregion
     
         #region Serialized Variables
         
         [SerializeField] 
         private MeshFilter meshFilter;
         [SerializeField] 
         private MeshFilter sideMeshFilter;
         #endregion
     
         #region Private Variables
         private WeaponData _weaponData;
         private bool _hasSideMesh;
         private int _damage;
         private float _attackRate;
         private int _weaponLevel;
         #endregion
         #endregion
         public void SetWeaponData(WeaponData weaponData)
         {
             _weaponData = weaponData;
             SetData();
         }
         private void SetData()
         {
             meshFilter.mesh = _weaponData.WeaponMesh;
             _damage = _weaponData.Damage;
             _attackRate = _weaponData.AttackRate;
             _weaponLevel = _weaponData.WeaponLevel;
             _hasSideMesh = _weaponData.HasSideMesh;
             if (!_weaponData.HasSideMesh) return;
             sideMeshFilter.mesh = _weaponData.SideMesh;
         }
    }
}