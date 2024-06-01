using CodeBase.Infrastructure.Factories;
using CodeBase.Player;
using CodeBase.Services;
using System.Linq;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class Attack : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private float _cooldown;
        [SerializeField] private Transform _attackPoint;
        [SerializeField] private float _attackRadius;
        [SerializeField] private float _damage;

        private Transform _player;
        private IGameFactory _gameFactory;
        private float _currentCooldown;
        private bool _isAttacking;
        private Collider[] _playerCollider = new Collider[1];
        private bool _isEnable;
        private int _playerLayer;

        private void Awake()
        {
            _playerLayer = 1 << LayerMask.NameToLayer("Player");
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
            MinusCooldown();

            if (CanAttack())
                StartAttack();
        }
        public void Disable() => 
            _isEnable = false;

        public void Enable() => 
            _isEnable = true;

        private bool CanAttack() =>
            CooldownIsUp() && _isAttacking == false && _isEnable;

        private bool CooldownIsUp() =>
            _currentCooldown <= 0;

        private void StartAttack()
        {
            _isAttacking = true;
            _animator.PlayAttack();
        }

        private void MinusCooldown()
        {
            if (!CooldownIsUp()) 
                _currentCooldown -= Time.deltaTime;
        }

        public void OnAttack()
        {
            if (Hit(out Collider hit))
                hit.transform.GetComponent<PlayerHealth>().TakeDamage(_damage);
        }

        private bool Hit(out Collider hit)
        {
            int count = Physics.OverlapSphereNonAlloc(_attackPoint.position, _attackRadius, _playerCollider, _playerLayer);
            hit = _playerCollider.FirstOrDefault();
            return count != 0;
        }

        public void OnAttackEnd() => 
            _isAttacking = false;

        private void OnDrawGizmosSelected()
        {
            if (_attackPoint == null) return;
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_attackPoint.position, _attackRadius);
        }
    }
}
