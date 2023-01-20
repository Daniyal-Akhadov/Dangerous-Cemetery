using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class AnimateAlongAgent : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private EnemyAnimator _animator;

        private bool ShouldMove =>
            _agent.remainingDistance > _agent.stoppingDistance;

        private void Update()
        {
            if (ShouldMove)
                _animator.Move(_agent.velocity.magnitude);
            else
                _animator.StopMoving();
        }
    }
}