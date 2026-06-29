using System;
using UnityEngine;

namespace Game.Gameplay
{
    public interface IHealthComponent
    {
        public bool HasHealth { get; }
        public event Action OnHealthEmpty;
        public event Action OnHit;
        public event Action<int, int> OnHealthChanged;
        public void ReceiveDamage(DamageComponent component);
        public void Initialize();
    }
    
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
        
        public event Action OnHealthEmpty;
        public event Action OnHit;
        public event Action<int, int> OnHealthChanged;
        public bool HasHealth => _currentHealth > 0;
        
        private ICondition _condition;
        private readonly Settings _settings;
        private int _currentHealth;
        
        public HealthComponent(Settings settings)
        {
            _settings = settings;
            _currentHealth = _settings.MaxHealth;
        }

        public void Initialize() => _currentHealth = _settings.MaxHealth;
        
        public void SetCondition(ICondition condition) => _condition = condition;
        
        public void ReceiveDamage(DamageComponent component)
        {
            _currentHealth = Mathf.Clamp(_currentHealth - component.damage, 0,  _settings.MaxHealth);
            OnHit?.Invoke();
            OnHealthChanged?.Invoke(_currentHealth,  _settings.MaxHealth);
            if (_currentHealth == 0)
            {
                this.OnHealthEmpty?.Invoke();
            }
        }
    }
}