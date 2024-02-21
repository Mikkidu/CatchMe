using System;
using UnityEngine;
using UnityEngine.AI;

namespace AlexDev.CatchMe   
{
    public class UnitMovementController
    {
        public event Action<float> OnVelocityUpdate;

        private NavMeshAgent _agent;

        public Vector3 currentPosition { get; private set; }

        public UnitMovementController(NavMeshAgent navMeshAgent)
        {
            _agent = navMeshAgent;
        }

        public void Update()
        {
            OnVelocityUpdate?.Invoke(_agent.velocity.magnitude);
        }

        public void MoveToPoint(Vector3 destination)
        {
            _agent.destination = destination;
        }

        public bool IsDestinationReachedOrStopped()
        {
            bool isWithinStoppingDistance = _agent.remainingDistance <= _agent.stoppingDistance;
            bool hasNoPathOrZeroVelocity = !_agent.hasPath || _agent.velocity.sqrMagnitude == 0f;

            return !_agent.pathPending && isWithinStoppingDistance && hasNoPathOrZeroVelocity;
        }

        public void StopUnitMovement()
        {
            if (!_agent.isStopped) MoveToPoint(_agent.transform.position);
        }

        public void ContinueUnitMovement()
        {
            if (_agent.isStopped) _agent.isStopped = false;
        }

    }
}
