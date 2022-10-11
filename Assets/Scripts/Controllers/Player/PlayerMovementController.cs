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

        #region Serialized Variables

        [SerializeField]
        private new Rigidbody rigidbody;
        [SerializeField]
        private PlayerManager manager;
        
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

        public void RotatePlayerToTarget(Transform enemyTarget)
        {
            transform.LookAt(enemyTarget, Vector3.up*3f);
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
                if (!manager.HasEnemyTarget)
                {
                    RotatePlayer();
                }
            }
            else if(rigidbody.velocity != Vector3.zero)
            {
                rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
            }
        }
        private void RotatePlayer()
        {
            Vector3 movementDirection = new Vector3(_inputVector.x, 0, _inputVector.y);
            if (movementDirection == Vector3.zero) return;
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            rigidbody.rotation = Quaternion.RotateTowards(rigidbody.rotation, toRotation,30);
        }
    }
}