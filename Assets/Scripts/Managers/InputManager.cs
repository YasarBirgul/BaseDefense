using System;
using Data.UnityObject;
using Data.ValueObject.InputData;
using Keys;
using Signals;
using UnityEngine;

namespace Managers
{ 
    public class InputManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables,
        
        [SerializeField] private FloatingJoystick joystickInput;
        
        #endregion

        #region Private Variables

        private InputData _data;
        private Vector2 _inputValues = Vector2.zero;
        
        #endregion
        
        #endregion
        private void Awake()
        {
            _data = GetInputData();
        }
        private InputData GetInputData() => Resources.Load<CD_Input>("Data/CD_Input").InputData;
        private void Update()
        {
            JoystickInputUpdate();
        }
        private void JoystickInputUpdate()
        {
            float horizontalInputDeltaPosition = Math.Abs(joystickInput.Horizontal - _inputValues.x);
            float verticalInputDeltaPosition = Math.Abs(joystickInput.Vertical - _inputValues.y);
            
            if(horizontalInputDeltaPosition > _data.InputPrecision || verticalInputDeltaPosition > _data.InputPrecision)
            { 
                _inputValues = new Vector2(joystickInput.Horizontal,joystickInput.Vertical) ;
                InputSignals.Instance.onInputDragged?.Invoke(new HorizontalInputParams()
                {
                    InputVector = _inputValues,
                });
            }
        }
    }
}