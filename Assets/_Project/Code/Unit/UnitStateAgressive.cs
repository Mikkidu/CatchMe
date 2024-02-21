using UnityEngine;

namespace AlexDev.CatchMe
{
    public class UnitStateAgressive : IUnitState
    {
        private UnitController _controller;
        private float _attackDistance = 1.25f;
        private float _attackInterval = 1;
        private float _attackTrigger;
        private bool _isAttacking;

        public UnitStateAgressive(UnitController controller)
        {
            _controller = controller;
            Debug.Log($"{_controller.name} switched to Agressive State");
        }

        public void Update()
        {
            if (_controller.TargetTransform == null || !_controller.isTagger)
            {
                ContinueUnitMovement();
                ChangeState();
                return;
            }

            StalkEnemy();
        }

        private void StalkEnemy()
        {
            if (Vector3.Distance(_controller.transform.position, _controller.TargetTransform.position) > _attackDistance)
            {
                if (_isAttacking)
                {
                    _isAttacking = false;
                }
                _controller.MoveToPoint(_controller.TargetTransform.position);
            }
            else
            {
                if (!_isAttacking)
                {
                    _isAttacking = true;
                }
                Attack();
                _controller.transform.LookAt(_controller.TargetTransform);
            }
        }

        private void StopUnitMovement()
        {
            _controller.StopUnitMovement();
        }

        public void ContinueUnitMovement()
        {
            _controller.ContinueUnitMovement();
        }


        private void Attack()
        {
            if (Time.realtimeSinceStartup > _attackTrigger)
            {
                _controller.Tag();
                _attackTrigger = Time.realtimeSinceStartup + _attackInterval;
            }
        }

        private void ChangeState()
        {
            _controller.state = new UnitStatePatrol(_controller);
        }

        public void Catched()
        {
            
        }
    }
}
