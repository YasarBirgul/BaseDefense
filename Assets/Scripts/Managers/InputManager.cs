using System;
using Data.ValueObject.InputData;
using Enums.Input;
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

        #region Serialized Variables
        
        [SerializeField] 
        private FloatingJoystick joystickInput;
        
        #endregion

        #region Private Variables

        private InputData _data;
        private bool _hasTouched;
        private InputHandlers _inputHandlers = InputHandlers.Character;
        private bool _readyToPlay;

        #endregion
        
        #endregion
        
        #region Event Subscriptions
        private void OnEnable()
        {
            SubscribeEvents();
        } 
        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputHandlerChange += OnInputHandlerChange;
            CoreGameSignals.Instance.onReadyToPlay += OnReadyToPlay;
            CoreGameSignals.Instance.onReset += OnReset;
        }
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onReset -= OnReset;
            InputSignals.Instance.onInputHandlerChange -= OnInputHandlerChange;
            CoreGameSignals.Instance.onReadyToPlay -= OnReadyToPlay;
        }

        private void OnInputHandlerChange(InputHandlers inputHandlers)
        {
            _inputHandlers = inputHandlers;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion Event Subscriptions
        private void Update()
        { 
            if (_readyToPlay) 
                JoystickInputUpdate();
        } 
        private void OnReadyToPlay()
        {
            _readyToPlay = true;
        }
        private void JoystickInputUpdate()
        {
            if (Input.GetMouseButton(0) && !_hasTouched)
            {
                _hasTouched = true;
            }
            if (!_hasTouched) return;
            HandleJoystickInput();
            _hasTouched = joystickInput.Direction.sqrMagnitude > 0;
        }
        #region JoystickInputChange
        private void HandleJoystickInput()
        {
            switch (_inputHandlers)
            {
                case InputHandlers.Character:
                    InputSignals.Instance.onInputDragged?.Invoke(new HorizontalInputParams()
                    {
                        MovementVector = new Vector2(joystickInput.Horizontal, joystickInput.Vertical)
                    });
                    break;
                
                case InputHandlers.Turret when joystickInput.Vertical <= -0.6f:                                      
                    _inputHandlers = InputHandlers.Character;                                                 
                    CoreGameSignals.Instance.onCharacterInputRelease?.Invoke();
                    return;
                
                case InputHandlers.Turret:
                    InputSignals.Instance.onJoystickInputDraggedforTurret?.Invoke(new HorizontalInputParams()
                    {
                        MovementVector = new Vector2(joystickInput.Horizontal, joystickInput.Vertical)
                    });
                    if (joystickInput.Direction.sqrMagnitude != 0)
                    {
                        InputSignals.Instance.onInputDragged?.Invoke(new HorizontalInputParams()
                        {
                            MovementVector = Vector2.zero
                        });
                    }
                    break;
                case InputHandlers.Drone:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        #endregion
        private void OnReset()
        {
            _readyToPlay = false;
        }
    }
}