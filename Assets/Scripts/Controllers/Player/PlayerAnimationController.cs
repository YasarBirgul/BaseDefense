using Enums.Player;
using Keys;
using UnityEngine;

namespace Controllers
{
    public class PlayerAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables,

        #endregion

        #region Private Variables

        private Animator animator;

        private PlayerAnimationStates currentAnimationState;

        #endregion

        #endregion
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
        public void PlayAnimation(HorizontalInputParams inputParams)
        {
            ChangeAnimations( inputParams.InputVector.magnitude > 0
                ? PlayerAnimationStates.Run
                : PlayerAnimationStates.Idle);
        }
        private void ChangeAnimations(PlayerAnimationStates animationStates)
        {
            if (animationStates == currentAnimationState) return;
            animator.Play(animationStates.ToString());
            currentAnimationState = animationStates;
        }
    }
}