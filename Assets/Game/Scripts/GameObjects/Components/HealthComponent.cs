using System;
using UnityEngine;

namespace Game.Components
{
    [Serializable]
    public class HealthComponent : MonoBehaviour
    {
        public event Action OnDamageTaken;
        
        public event Action HealthDepleted;
        
        public event Action<int, int> OnHealthChanged;


        public bool HasHealth => _currentHealth > 0;
        
        [SerializeField]
        private int _maxHealth = 5;
        
        private int _currentHealth;
        
        private void OnEnable()
        {
            _currentHealth = _maxHealth;
        }

        public void ReceiveDamage(int damage)
        {
            _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maxHealth);
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
            this.OnDamageTaken?.Invoke();
            if (_currentHealth <= 0)
                this.HealthDepleted?.Invoke();
        }
    }
}