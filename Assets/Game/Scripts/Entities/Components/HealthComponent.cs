using System;
using Game.Scripts.Context.GameObject.Ship;
using UnityEngine;

namespace Game.Gameplay
{
    public interface IHealthComponent
    {
        public bool HasHealth { get; }
        public event Action<Entity> OnHealthDepleted;
        public event Action OnHit;
        public event Action<int, int> OnHealthChanged;
        public void ReceiveDamage(DamageComponent component);
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
        
        public event Action<Entity> OnHealthDepleted;
        public event Action OnHit;
        public event Action<int, int> OnHealthChanged;

        public bool HasHealth => _currentHealth > 0;
        private int _currentHealth;
        private ICondition _condition;
        private readonly Settings _settings;
        private readonly Entity _entity;
        
        public HealthComponent(Entity entity, Settings settings)
        {
            _entity = entity;
            _settings = settings;
            _currentHealth = _settings.MaxHealth;
        }

        public void SetCondition(ICondition condition) => _condition = condition;
        
        public void ReceiveDamage(DamageComponent component)
        {
            _currentHealth = Mathf.Clamp(_currentHealth - component.damage, 0,  _settings.MaxHealth);
            OnHit?.Invoke();
            OnHealthChanged?.Invoke(_currentHealth,  _settings.MaxHealth);
            if (_currentHealth <= 0)
                this.OnHealthDepleted?.Invoke(_entity);
        }
    }
}