using CodeBase.Infrastructure.Factories;
using CodeBase.Services;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class LookFollowPlayer : Follow
    {
        [SerializeField] private float _speed;

        private Transform _player;
        private IGameFactory _gameFactory;
        private Vector3 _positionToLook;

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

        private void Update()
        {
            if (_player != null)
                RotateTowardToPlayer();
        }

        private void RotateTowardToPlayer()
        {
            UpdatePositionToLookAt();

            transform.rotation = SmoothedRotation(transform.rotation, _positionToLook);
        }

        private Quaternion SmoothedRotation(Quaternion rotation, Vector3 position) =>
            Quaternion.Lerp(rotation, TargetRotation(position), _speed * Time.deltaTime);

        private Quaternion TargetRotation(Vector3 position) =>
            Quaternion.LookRotation(_positionToLook);

        private void UpdatePositionToLookAt()
        {
            Vector3 positionDifference = _player.position - transform.position;
            _positionToLook = new Vector3(positionDifference.x, transform.position.y, positionDifference.z);
        }
    }
}
