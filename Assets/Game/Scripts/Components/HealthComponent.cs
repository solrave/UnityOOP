using System;
using UnityEngine;
using Zenject;

namespace Game.Components
{
    [Serializable]
    public class HealthComponent : IHealthComponent
    {
        public event Action OnHealthDepleted;
        public event Action<int, int> OnHealthChanged;

        public bool HasHealth => _currentHealth > 0;
        private int _maxHealth;
        private int _currentHealth;

        [Inject]
        public HealthComponent(int maxHealth)
        {
            _maxHealth = maxHealth;
            _currentHealth = _maxHealth;
        }

        public void ReceiveDamage(int damage)
        {
            _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maxHealth);
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
            if (_currentHealth <= 0)
                this.OnHealthDepleted?.Invoke();
        }
    }

    public interface IHealthComponent
    {
        public bool HasHealth { get; }
        public event Action OnHealthDepleted;
        public event Action<int, int> OnHealthChanged;
        public void ReceiveDamage(int damage);
    }
}