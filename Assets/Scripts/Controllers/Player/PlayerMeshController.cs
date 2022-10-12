using Data.ValueObject.WeaponData;
using Enums.GameStates;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables,
         
        [SerializeField]
        private Transform manager;
        [SerializeField] 
        private MeshRenderer weaponMeshRenderer;
        [SerializeField]
        private MeshRenderer sideMeshRenderer;

        #endregion

        #region Private Variables

        private WeaponData _data;
        
        #endregion

        #endregion
        
        
        public void SetWeaponData(WeaponData weaponData)
        {
            _data = weaponData;
        }
        
        public void ChangeAreaStatus(AreaType areaStatus)
        {
            if (areaStatus == AreaType.BaseDefense)
            {
                weaponMeshRenderer.enabled = false;
                sideMeshRenderer.enabled = false;
            }
            else
            {
                weaponMeshRenderer.enabled = true;
                sideMeshRenderer.enabled = _data.HasSideMesh;
            }
        }
    }
}