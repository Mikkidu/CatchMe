using System;
using UnityEngine;
using UnityEngine.AI;

namespace AlexDev.CatchMe
{

    public class UnitController : MonoBehaviour
    {
        [SerializeField] private float _patrolAreaSize = 30;
        [SerializeField] private Transform _targetTransform;
        public AudioClip[] footstepAudioClips;
        public AudioClip landingAudioClip;

        private UnitMovementController _movementController;
        private UnitAnimationController _animationController;
        private UnitAudioController _audioController;
        private Tagging _tagger;

        public IUnitState state;

        public bool isTagger { get; private set; } = false;

        [HideInInspector] public bool isLocked;
        [HideInInspector] public Vector3 currentPosition;

        public float PatrolAreaSize => _patrolAreaSize;
        public Transform TargetTransform => _targetTransform;

        private void Awake()
        {
            currentPosition = Vector3.zero;

            InitializeControllers();

            state = new UnitStatePatrol(this);

        }

        private void InitializeControllers()
        {
            var agent = GetComponent<NavMeshAgent>();
            var animator = GetComponent<Animator>();
            _tagger = GetComponent<Tagging>();
            //enemyTargetsList = new List<Transform>();

            _movementController = new UnitMovementController(agent);
            _animationController = new UnitAnimationController(this, animator);
            _audioController = new UnitAudioController(this);

        }

        private void Update()
        {
            if (isLocked && IsDestinationReachedOrStopped())
            {
                isLocked = false;
            }

            state.Update();
            _movementController.Update();
        }

        #region AudioController

        private void OnFootstep(AnimationEvent animationEvent)
        {
            _audioController.OnFootstep(animationEvent);
        }

        private void OnLand(AnimationEvent animationEvent)
        {
            _audioController.OnLand(animationEvent);
        }

        #endregion

        #region Animator

        public void UpdateGrounding(bool isGrounded)
        {

            _animationController.UpdateGrounding(isGrounded);
        }

        public void UpdateSpeed(float speed)
        {
            _animationController.UpdateSpeed(speed);
        }

        public void Grounded()
        {
            _animationController.Grounded();
        }

        public void Jump()
        {
            _animationController.Jump();
        }

        public void Fall()
        {
            _animationController.Fall();
        }

        #endregion

        #region Tagging

        public void Catched()
        {
            isTagger = true;
            state.Catched();
            Debug.Log(name + " catched");
        }

        public void Tag()
        {
            _tagger.Tag();
        }

        public void OnTagSucces()
        {
            isTagger = false;
        }

        #endregion

        #region MovementInterface

        public void SubscribeOnSpeedUpdate(Action<float> speedDelegate)
        {
            _movementController.OnVelocityUpdate += speedDelegate;
        }

        public void UnubscribeOnSpeedUpdate(Action<float> speedDelegate)
        {
            _movementController.OnVelocityUpdate -= speedDelegate;
        }

        public void MoveToPoint(Vector3 point)
        {
            _movementController.MoveToPoint(point);
        }

        public void BackToCurrentPosition()
        {
            _movementController.MoveToPoint(currentPosition);
        }

        public void StopUnitMovement()
        {
            _movementController.StopUnitMovement();
        }

        public void ContinueUnitMovement()
        {
            _movementController.ContinueUnitMovement();
        }

        public bool IsDestinationReachedOrStopped()
        {
            return _movementController.IsDestinationReachedOrStopped();
        }

        #endregion

    }
}
