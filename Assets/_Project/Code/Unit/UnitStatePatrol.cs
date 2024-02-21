using System.Collections;
using UnityEngine;

namespace AlexDev.CatchMe
{
    class UnitStatePatrol : IUnitState
    {
        private UnitController _controller;
        private float _patrolAreaSize;
        private Vector3 _currentPosition;
        private Coroutine _waitCoroutine;

        private bool _isWaiting;

        public UnitStatePatrol(UnitController controller)
        {
            _controller = controller;
            Debug.Log($"{_controller.name} switched to Patrol State");
            _patrolAreaSize = _controller.PatrolAreaSize;
            _currentPosition = _controller.currentPosition;

        }

        public void Update()
        {
            /*if (_controller.enemyTargetsList.Count > 0)
            {
                _controller.state = new UnitStateAgressive(_controller);
            }*/
            if (!_isWaiting && _controller.IsDestinationReachedOrStopped())
            {
                _isWaiting = true;
                _waitCoroutine = _controller.StartCoroutine(WaitAndPatrol());
            }
        }

        private IEnumerator WaitAndPatrol()
        {
            float waitInterval = Random.Range(1f, 3f);
            yield return null;// new WaitForSeconds(waitInterval);
            _isWaiting = false;
            Patrol();
        }

        private IEnumerator ChangeStateCoroutine()
        {
            yield return new WaitForSeconds(1);
            _isWaiting = false;
            ChangeState();
        }

        private void Patrol()
        {
            MoveToRandomPoint();
        }

        /// <summary>
        /// Interaction with the navigator. Separate it into a separate block. Facade?
        /// </summary>
        public void MoveToRandomPoint()
        {
            _controller.MoveToPoint(GetRandomPatrolPoint());
        }

        /// <summary>
        /// Tool. Returns a random point in the specified patrol area. Needs to be separated into an external toolkit
        /// </summary>
        /// <returns></returns>
        private Vector3 GetRandomPatrolPoint()
        {
            float randomX = Random.Range(0, _patrolAreaSize) - _patrolAreaSize / 2 + _currentPosition.x;
            float randomZ = Random.Range(0, _patrolAreaSize) - _patrolAreaSize / 2 + _currentPosition.z;

            return new Vector3(randomX, _currentPosition.y, randomZ);
        }

        public void Catched()
        {
            _waitCoroutine = _controller.StartCoroutine(ChangeStateCoroutine());
        }

        private void ChangeState()
        {
            _controller.state = new UnitStateAgressive(_controller);
        }
    }
}
