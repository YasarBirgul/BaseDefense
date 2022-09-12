using Enums.GameStates;
using Enums.Player;
using Keys;
using Managers;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Controllers
{
    public class PlayerAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerManager playerManager;


        [SerializeField] private Rig twohandHoldingState;
        [SerializeField] private TwoBoneIKConstraint leftHandIK; 
        [SerializeField] private TwoBoneIKConstraint rightHandIK;
        [SerializeField] private GameObject pistolGun;
        
        #endregion

        #region Private Variables

        private Animator _animator;
        
        private PlayerAnimationStates _currentAnimationState;

        private float _velocityX, _velocityZ;

        private float _acceleration, _decelaration;

        #endregion

        #endregion
        private void Awake()
        {
            Init();
            
        }

        private void Init()
        {
            _animator = GetComponent<Animator>();
            leftHandIK.weight = 0;
            rightHandIK.weight = 0;
            _animator.SetLayerWeight(1, 0);
        }

        public void PlayAnimation(HorizontalInputParams inputParams)
        {
            if (playerManager.CurrentGameState == GameStates.AttackField)
            {
                pistolGun.SetActive(true);
                _animator.SetLayerWeight(1, 1);
                twohandHoldingState.weight = 1;
                rightHandIK.weight = 1;
                leftHandIK.weight = 1;
                _animator.SetBool("IsBattleOn",true);
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

                if (inputParams.MovementVector.sqrMagnitude == 0)
                {
                    twohandHoldingState.weight = 0;
                    leftHandIK.weight = 0;
                }
                _animator.SetFloat("VelocityZ",_velocityZ);
                _animator.SetFloat("VelocityX",_velocityX);
            }
            else
            {
                pistolGun.SetActive(false);
                _animator.SetLayerWeight(1, 0);
                twohandHoldingState.weight = 0;
                leftHandIK.weight = 0;
                rightHandIK.weight = 0;
                _animator.SetBool("IsBattleOn",false);
                ChangeAnimations( inputParams.MovementVector.magnitude > 0
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