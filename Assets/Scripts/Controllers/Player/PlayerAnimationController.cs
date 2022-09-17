﻿using Enums;
using Enums.GameStates;
using Enums.Player;
using Keys;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class PlayerAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables
        
        [SerializeField] private PlayerManager playerManager;
        #endregion

        #region Private Variables

        private Animator _animator;
        
        private PlayerAnimationStates _currentAnimationState;

        private float _velocityX, _velocityZ;

        private float _acceleration, _decelaration;

        public WeaponTypes CurrentWeaponType;
        
        #endregion

        #endregion
        private void Awake()
        {
            Init();
        }
        private void Init()
        {
            _animator = GetComponent<Animator>();
            _animator.SetLayerWeight(1, 0);
        }

        public void PlayAnimation(HorizontalInputParams inputParams)
        {
            if (playerManager.CurrentGameState == GameStates.AttackField)
            {
                if (_animator.GetBool("Aimed") == false)
                {
                    _animator.SetBool("Aimed",true);
                }
                _velocityX = inputParams.MovementVector.x;
                _velocityZ = inputParams.MovementVector.y;
                if (_velocityZ < 0.1f)
                {
                    _velocityZ += Time.deltaTime * _acceleration;
                }
                if (_velocityX > -0.1f && Mathf.Abs(_velocityZ) <= 0.2f)
                {
                    _velocityX -= Time.deltaTime * _acceleration;
                }
                if (_velocityX < 0.1f && Mathf.Abs(_velocityZ) <= 0.2f)
                {
                    _velocityX += Time.deltaTime * _acceleration;
                }
                if (_velocityZ > 0.0f)
                {
                    _velocityZ -= Time.deltaTime * _decelaration;
                }
                if (_velocityX < 0.0f)
                {
                    _velocityX += Time.deltaTime * _decelaration;
                }
                if (_velocityX > 0.0f)
                {
                    _velocityX -= Time.deltaTime * _decelaration;
                }
                if ( _velocityX!= 0.0f &&(_velocityX > -0.05f && _velocityX<0.05f))
                {
                    _velocityX = 0.0f;
                }
                _animator.SetFloat("VelocityZ",_velocityZ);
                _animator.SetFloat("VelocityX",_velocityX);
            }
            else
            {
                ChangeAnimations( inputParams.MovementVector.sqrMagnitude > 0
                    ? PlayerAnimationStates.Run
                    : PlayerAnimationStates.Idle);
            }
        }
        private void ChangeAnimations(PlayerAnimationStates animationStates)
        {
            if (animationStates == _currentAnimationState) return;
             _animator.Play(animationStates.ToString());
            _currentAnimationState = animationStates;
        }
    }
}