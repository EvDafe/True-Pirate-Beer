using CodeBase.Data;
using CodeBase.Services.Progress;
using System;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerHealth : MonoBehaviour, ISavedProgress
    {
        private PlayerState _playerState;

        public float CurrentHealth
        {
            get => _playerState.CurrentHealth;
            set
            {
                _playerState.CurrentHealth = value;
                HealthChanged?.Invoke();
            }
        }

        public float MaxHealth
        {
            get => _playerState.MaxHealth;
            set => _playerState.MaxHealth = value;
        }

        public event Action HealthChanged;

        public void LoadProgress(PlayerProgress progress)
        {
            _playerState = progress.PlayerState;
            HealthChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.PlayerState.CurrentHealth = CurrentHealth;
            progress.PlayerState.MaxHealth = MaxHealth;
        }

        public void TakeDamage(float damage)
        {
            if (damage < 0)
                throw new ArgumentException("Ебать ты слабый", nameof(damage));

            if (CurrentHealth <= 0) return;

            Debug.Log("Damage Taken");
            CurrentHealth -= damage;
        }
    }
}
