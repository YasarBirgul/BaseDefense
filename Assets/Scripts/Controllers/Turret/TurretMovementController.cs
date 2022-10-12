using Enums.Turret;
using Keys;
using UnityEngine;

namespace Controllers.Turret
{
    public class TurretMovementController : MonoBehaviour
    {
        [SerializeField] 
        private TurretLocationType turretLocationType;
        
        private float _horizontalInput;
        private float _verticalInput;
        private Vector2 _rotateDirection;
        public void SetInputParams(HorizontalInputParams input)
        {
            _horizontalInput = input.MovementVector.x;
            _verticalInput = input.MovementVector.y;
            Rotate();
        }
        private void Rotate()
        {
            _rotateDirection = new Vector2(_horizontalInput, _verticalInput).normalized;
            if (_rotateDirection.sqrMagnitude == 0)
                return;

            float angle = Mathf.Atan2(_rotateDirection.x,_rotateDirection.y) * Mathf.Rad2Deg;

            if (!(angle < 60) || !(angle > -60)) return;

            transform.rotation = Quaternion.Euler(new Vector3(0,angle,0));
        }
    }
}