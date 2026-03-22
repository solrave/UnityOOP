using System;
using UnityEngine;

namespace Game.Components
{
    [Serializable]
    public class HealthComponent : MonoBehaviour
    {
        public bool HasHealth => _currentHealth > 0;
        
        [SerializeField]
        private int _maxHealth = 5;
        
        private int _currentHealth;
        
        private void Awake()
        {
            _currentHealth = _maxHealth;
        }

        public void ReceiveDamage(int damage)
        {
            _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maxHealth);
        }
    }
}