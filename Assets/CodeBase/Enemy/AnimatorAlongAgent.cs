using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class AnimatorAlongAgent : MonoBehaviour
    {
        private const float MinimumVelocity = 0.1f;
        
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private EnemyAnimator _animator;

        private void OnValidate()
        {
            _agent ??= GetComponent<NavMeshAgent>();
            _animator ??= GetComponent<EnemyAnimator>();
        }

        private void Update()
        {
            if (ShouldMove())
                _animator.Move(_agent.velocity.magnitude);
            else
                _animator.StopMoving();
        }

        private bool ShouldMove() => 
            _agent.velocity.magnitude > MinimumVelocity && _agent.remainingDistance > _agent.radius;
    }
}
