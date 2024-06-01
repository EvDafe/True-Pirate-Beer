using CodeBase.Tests;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerDeath : MonoBehaviour
    {
        [SerializeField] private PlayerHealth _health;
        [SerializeField] private InputTest _movement;

        private bool _isDead;

        private void Start() => 
            _health.HealthChanged += CheackHealth;

        private void CheackHealth()
        {
            if (_health.CurrentHealth <= 0 && _isDead == false)
                Die();
        }

        private void Die()
        {
            _isDead = true;

            _movement.Disactive();
        }
    }
}
