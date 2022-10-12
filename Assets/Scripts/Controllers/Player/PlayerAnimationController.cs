﻿using System.Collections.Generic;
using Enums;
using Enums.GameStates;
using Enums.Player;
using Keys;
using Managers;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables

        [SerializeField] 
        private PlayerManager playerManager;
        
        [SerializeField] 
        private Animator animator;
        
        #endregion

        #region Private Variables
        
        private PlayerAnimationStates _currentAnimationState;

        private float _velocityX, _velocityZ;

        private float _acceleration, _decelaration;

        private Dictionary<WeaponTypes, PlayerAnimationStates> _animationStatesMap;

        #endregion

        #endregion
        private void Awake()
        {
            Init();
            DefineDictionary();
        }
        private void DefineDictionary()
        {
            _animationStatesMap = new Dictionary<WeaponTypes, PlayerAnimationStates>()
            {
                {WeaponTypes.Pistol, PlayerAnimationStates.Pistol},
                {WeaponTypes.Riffle, PlayerAnimationStates.Riffle},
                {WeaponTypes.ShotGun, PlayerAnimationStates.ShotGun},
                {WeaponTypes.MiniGun, PlayerAnimationStates.MiniGun},
            };
        }
        private void Init()
        {
            animator = GetComponent<Animator>();
        }

        public void PlayTurretAnimation(bool onTurretHold)
        {
            animator.SetLayerWeight(2,onTurretHold ? 1 : 0);
        }
        
        public void PlayAnimation(HorizontalInputParams inputParams)
        { 
            if (playerManager.CurrentAreaType == AreaType.BattleOn)
            {
                animator.SetLayerWeight(1,1);
                animator.SetBool("IsBattleOn",true);
                ChangeAnimations(_animationStatesMap[playerManager.WeaponType]);
                animator.SetBool("Aimed",true);
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
                
                animator.SetFloat("VelocityX",_velocityX);
                animator.SetFloat("VelocityZ",_velocityZ);

                if (inputParams.MovementVector.sqrMagnitude == 0)
                {
                    AimTarget(playerManager.EnemyTarget);
                }
            }
            else
            {
                animator.SetBool("Aimed",false);
                animator.SetLayerWeight(1,0);
                animator.SetBool("IsBattleOn",false);
                ChangeAnimations( inputParams.MovementVector.sqrMagnitude > 0
                    ? PlayerAnimationStates.Run
                    : PlayerAnimationStates.Idle);
            }
        }
        private void ChangeAnimations(PlayerAnimationStates animationStates)
        {
            if (animationStates == _currentAnimationState) return;
             animator.Play(animationStates.ToString());
            _currentAnimationState = animationStates;
        } 
        public void AimTarget(bool hasTarget)
        {
            animator.SetBool("Aimed",hasTarget);
        }
    }
}