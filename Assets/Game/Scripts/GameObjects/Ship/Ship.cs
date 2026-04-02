using System;
using Game.Components;
using Game.Scripts.Components;
using UnityEngine;

namespace Game
{
    public abstract class Ship : MonoBehaviour,IDamageable
    {
        public event Action<Ship> OnShipDestroyed;
        public event Action<int, int> OnHealthChanged;

        [SerializeField]
        protected FireComponent _fireComponent;
        
        [SerializeField]
        protected HealthComponent _healthComponent;
        
        [SerializeField]
        protected MoveComponent _moveComponent;
        
        [SerializeField]
        protected AnimationComponent animationComponent;
        
        private void OnEnable()
        {
            _fireComponent.OnFire += animationComponent.AnimateFire;
            _healthComponent.HealthDepleted += ShipDestroyed;
            _healthComponent.OnDamageTaken += animationComponent.AnimateDamage;
            _healthComponent.OnHealthChanged += HealthChanged;
            this.OnShipDestroyed += animationComponent.AnimateDestruction;
        }

        private void OnDisable()
        {
            _fireComponent.OnFire -= animationComponent.AnimateFire;
            _healthComponent.HealthDepleted -= ShipDestroyed;
            _healthComponent.OnDamageTaken -= animationComponent.AnimateDamage;
            _healthComponent.OnHealthChanged -= HealthChanged;
            this.OnShipDestroyed -= animationComponent.AnimateDestruction;
        }
        
        protected void FixedUpdate() => Proceed();
        
        public void Fire()
        {
            if (_healthComponent.HasHealth)
            {
                _fireComponent.FireUp();
            }
        }

        public void SetDestination(Vector2? position) => _moveComponent.SetDirection(position);

        public void TakeDamage(int damage) => _healthComponent.ReceiveDamage(damage);
        
        protected abstract void Proceed();

        private void ShipDestroyed()
        { 
            OnShipDestroyed?.Invoke(this);
            this.gameObject.SetActive(false);
        }

        private void HealthChanged(int currentHealth, int maxHealth) =>
            OnHealthChanged?.Invoke(currentHealth, maxHealth);

    }

    public interface IDamageable
    {
        public void TakeDamage(int damage);
    }
}