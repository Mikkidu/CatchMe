using UnityEngine;

namespace AlexDev.CatchMe
{
    public class UnitAnimationController
    {
        private Animator _animator;
        private UnitController _controller;

        private bool _isGrounded = false;

        // animation IDs
        private int _animIDSpeed;
        private int _animIDGrounded;
        private int _animIDJump;
        private int _animIDFreeFall;
        private int _animIDMotionSpeed;


        public UnitAnimationController( UnitController controller, Animator animator)
        {
            _animator = animator;
            _controller = controller;
            _controller.SubscribeOnSpeedUpdate(UpdateSpeed);
            AssignAnimationIDs();
        }

        public void UpdateGrounding(bool isGrounded)
        {
            if (isGrounded != _isGrounded)
            {
                _animator.SetBool(_animIDGrounded, isGrounded);
                _isGrounded = isGrounded;
            }
        }

        public void UpdateSpeed(float _animationBlend)
        {
            _animator.SetFloat(_animIDSpeed, _animationBlend);
        }

        public void Grounded()
        {
            _animator.SetBool(_animIDJump, false);
            _animator.SetBool(_animIDFreeFall, false);
        }

        public void Jump()
        {
            _animator.SetBool(_animIDJump, true);
        }

        public void Fall()
        {
            _animator.SetBool(_animIDFreeFall, true);
        }

        private void AssignAnimationIDs()
        {
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDGrounded = Animator.StringToHash("Grounded");
            _animIDJump = Animator.StringToHash("Jump");
            _animIDFreeFall = Animator.StringToHash("FreeFall");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        }
    }
}
