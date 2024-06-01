using CodeBase.Services.Input;
using TMPro.Examples;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private float _cooldown;
        [SerializeField] private Transform _muzzle;
        [SerializeField] private Projectile _projectile;
        [SerializeField] private PlayerAnimator _animator;
        [SerializeField] private float _force;

        private float _currentCooldown;
        private PCInputService _inputService;

        private void Update()
        {
            MinusCooldown();

            if (CanAttack())
                StartAttack();
        }

        private bool CanAttack() =>
            CooldownIsUp() && _inputService.IsAttack();

        private bool CooldownIsUp() =>
            _currentCooldown <= 0;

        private void StartAttack()
        {
            _animator.PlayAttack();
        }

        private void MinusCooldown()
        {
            if (!CooldownIsUp())
                _currentCooldown -= Time.deltaTime;
        }

        public void OnAttack()
        {
            var raycast = Physics.Raycast(transform.position, transform.forward,out RaycastHit hit, 50f);
            var to = hit.point;
            Vector3 direction = (_muzzle.position - to).normalized;
            var projectile = Instantiate(_projectile, _muzzle.position, Quaternion.identity);
            projectile.transform.LookAt(to);
            projectile.RigidBody.AddForce(direction * _force, ForceMode.Impulse);
        }
    }

    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidBody;
        [SerializeField, Min(0)] private float _damage;
        [SerializeField] private ProjectileDisposeType _disposeType = ProjectileDisposeType.Manual;

        [SerializeField] private bool _spawnEffectOnDispose;
        [SerializeField] private ParticleSystem _onDisposeEffect;
        [SerializeField] private float _effectLifetime;

        private bool _isDisposed;

        public Rigidbody RigidBody => _rigidBody;

        private void OnValidate() => 
            _rigidBody ??= GetComponent<Rigidbody>();

        private void OnCollisionEnter(Collision collision)
        {
            
        }
    }

    public enum ProjectileDisposeType
    {
        OnAnyCollision,
        OnTargetCollision,
        Manual
    }
}
