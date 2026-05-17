using System;
using UnityEngine;

namespace Game.Scripts.Components.Core
{
    public class HealthComponent : IHealthComponent
    {
        [Serializable]
        public sealed class Settings
        {
            [field: SerializeField] 
            public int MaxHealth { get; private set; }
        }
        
        public interface ICondition
        {
            bool Evaluate();
        }
        
        public event Action OnHealthDepleted;
        public event Action OnHit;
        public event Action<int, int> OnHealthChanged;

        public bool HasHealth => _currentHealth > 0;
        private int _currentHealth;
        private ICondition _condition;
        private readonly Settings _settings;
        
        public HealthComponent(Settings settings)
        {
            _settings = settings;
            _currentHealth = _settings.MaxHealth;
        }

        public void SetCondition(ICondition condition) => _condition = condition;
        
        public void ReceiveDamage(int damage)
        {
            _currentHealth = Mathf.Clamp(_currentHealth - damage, 0,  _settings.MaxHealth);
            OnHit?.Invoke();
            OnHealthChanged?.Invoke(_currentHealth,  _settings.MaxHealth);
            if (_currentHealth <= 0)
                this.OnHealthDepleted?.Invoke();
        }
    }

    public interface IHealthComponent
    {
        public bool HasHealth { get; }
        public event Action OnHealthDepleted;
        public event Action OnHit;
        public event Action<int, int> OnHealthChanged;
        public void ReceiveDamage(int damage);
    }
}