using CodeBase.Infrastructure.Factories;
using CodeBase.Services;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AgentMoveToPlayer : Follow
    {
        private const float MinimalDistance = 1f;

        [SerializeField] private NavMeshAgent _agent;
        
        private Transform _player;

        private IGameFactory _gameFactory;

        private void Awake()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();
            if (_gameFactory.PlayerGameObject != null)
                _player = _gameFactory.PlayerGameObject.transform;
            else
                _gameFactory.PlayerCreated += InitializePlayerTransform;
        }

        private void InitializePlayerTransform() => 
            _player = _gameFactory.PlayerGameObject.transform;

        private void OnValidate() => 
            _agent ??= GetComponent<NavMeshAgent>();

        private void Update()
        {
            if(_player != null)
            {
                if (Vector3.Distance(transform.position, _player.position) <= MinimalDistance)
                    return;
                _agent.SetDestination(_player.position);
            }
        }
    }
}
