using CodeBase.GameLogic;
using UnityEngine;

namespace CodeBase.Player
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private ProgressBar _healthBar;

        private PlayerHealth _playerHealth;
        
        private void OnDestroy() => 
            _playerHealth.HealthChanged -= UpdateHealthBar;

        public void Construct(PlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;
            _playerHealth.HealthChanged += UpdateHealthBar;
        }

        private void UpdateHealthBar() => 
            _healthBar.UpdateProgress(_playerHealth.CurrentHealth, _playerHealth.MaxHealth);
    }
}
