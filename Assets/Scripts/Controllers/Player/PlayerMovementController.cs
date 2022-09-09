using Data.ValueObject.PlayerData;
using Keys;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables,
         
        [SerializeField] private PlayerManager playerManager;
        
        [SerializeField] private new Rigidbody rigidbody;
        
        #endregion

        #region Private Variables
        
        private PlayerMovementData _data;

        private Vector2 _inputVector;

        private bool _isReadyToMove;
        
        #endregion

        #endregion
        public void SetMovementData(PlayerMovementData movementData)
        {
            _data = movementData;
        }
        public void UpdateInputValues(HorizontalInputParams inputParams)
        {
            _inputVector = inputParams.MovementVector;
            EnableMovement(_inputVector.sqrMagnitude > 0);
        }
        private void EnableMovement(bool movementStatus)
        {
            _isReadyToMove = movementStatus;
        }
        private void FixedUpdate()
        {
            PlayerMove();
        }
        private void PlayerMove()
        {
            if (_isReadyToMove)
            {
                var velocity = rigidbody.velocity; 
                velocity = new Vector3(_inputVector.x,velocity.y, _inputVector.y)*_data.PlayerSpeed;
                rigidbody.velocity = velocity;
            }
            else if(rigidbody.velocity != Vector3.zero)
            {
                rigidbody.velocity = Vector3.zero;
            }
        }
    }
}