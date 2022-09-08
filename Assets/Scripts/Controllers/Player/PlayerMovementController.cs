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

        #endregion

        #endregion
        public void SetMovementData(PlayerMovementData movementData)
        {
            _data = movementData;
        }
        public void UpdateInputValues(HorizontalInputParams inputParams)
        {
            _inputVector = inputParams.InputVector;
        }
        private void FixedUpdate()
        {
            PlayerMove();
        }
        private void PlayerMove()
        {
            var velocity = rigidbody.velocity; 
            velocity = new Vector3(_inputVector.x,velocity.y, _inputVector.y)*_data.PlayerSpeed;
            rigidbody.velocity = velocity;
        }
    }
}