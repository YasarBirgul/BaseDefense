using Data.ValueObject.WeaponData;
using Enums.GameStates;
using Keys;
using UnityEngine;

namespace Controllers
{
    public class PlayerMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables,
         
        [SerializeField] private Transform manager;
        [SerializeField] private MeshRenderer weaponMeshRenderer;
        [SerializeField] private MeshRenderer sideMeshRenderer;

        #endregion

        #region Private Variables

        private WeaponData _data;
        
        #endregion

        #endregion
        public void SetWeaponData(WeaponData weaponData)
        {
            _data = weaponData;
        }
        public void LookRotation(HorizontalInputParams inputParams)
        {
            var movementDirection = new Vector3(inputParams.MovementVector.x, 0, inputParams.MovementVector.y);
            if (movementDirection == Vector3.zero) return;
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            manager.rotation = Quaternion.RotateTowards( manager.rotation,toRotation,30);
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