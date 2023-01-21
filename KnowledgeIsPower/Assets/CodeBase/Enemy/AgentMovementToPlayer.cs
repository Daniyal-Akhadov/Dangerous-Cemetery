using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    public class AgentMovementToPlayer : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;

        private Transform _hero;
        private bool _isMovementEnabled;

        private bool IsNotReached =>
            Vector3.Distance(transform.position, _hero.position) >= _agent.stoppingDistance;

        private bool CanMove =>
            _hero != null && _isMovementEnabled == true && IsNotReached == true;

        private void Update()
        {
            if (CanMove)
                _agent.destination = _hero.transform.position;
        }

        public void Enable()
        {
            _isMovementEnabled = true;
        }

        public void Disable()
        {
            _isMovementEnabled = false;
        }

        public void Construct(Transform heroTransform)
        {
            _hero = heroTransform;
        }
    }
}