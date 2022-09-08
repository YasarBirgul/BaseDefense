using System;
using Data.UnityObject;
using Data.ValueObject.InputData;
using Keys;
using Signals;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

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
        private float _inputPrecision;
        
        #endregion
        
        #endregion
        private void Awake()
        {
            _data = GetInputData();
            Init();
        }
        private InputData GetInputData() => Resources.Load<CD_Input>("Data/CD_Input").InputData;
        
        private void Init()
        {
            _inputPrecision = _data.InputPrecision;
        } 
        private void Update()
        {
            JoystickInputUpdate();
        }
        private void JoystickInputUpdate() 
        {
            var horizontalInputDelta = Math.Abs(joystickInput.Horizontal - _inputValues.x);
            var verticalInputDelta = Math.Abs(joystickInput.Vertical - _inputValues.y);

            if (!(horizontalInputDelta > _inputPrecision) && !(verticalInputDelta> _inputPrecision)) return;
            
            _inputValues = new Vector2(joystickInput.Horizontal,joystickInput.Vertical);
            InputSignals.Instance.onInputDragged?.Invoke(new HorizontalInputParams()
            {
                InputVector = _inputValues,
            });
        }
    }
}