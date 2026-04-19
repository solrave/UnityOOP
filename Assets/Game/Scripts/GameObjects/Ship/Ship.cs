using System;
using Game.Components;
using Game.Scripts.Components;
using UnityEngine;

namespace Game
{
    public sealed class Ship : MonoBehaviour,IDamageable
    {
        public event Action<Transform> OnFire;
        public event Action<Transform> OnShipDestroyed;
        public event Action<int, int> OnHealthChanged;
        public event Action OnDamageTaken;
        public event Action<Vector2?, float> OnMove;  
        
        public bool ReadyToShoot => _healthComponent.HasHealth;

        public Vector2 Position => transform.position;
        
        [field: SerializeField]
        public TeamType Team { get;  private set;}
        
        [SerializeField]
        private FireComponent _fireComponent;
        
        [SerializeField]
        private HealthComponent _healthComponent;
        
        [SerializeField]
        private MoveComponent _moveComponent;

        private void FixedUpdate() => _moveComponent.FixedUpdate();

        private void OnEnable()
        {
            _healthComponent.Init();
            _healthComponent.OnHealthDepleted += ShipDestroyed;
            _healthComponent.OnHealthChanged += HealthChanged;
            _moveComponent.OnMove += AnimateMovement;
            _fireComponent.OnFire += this.OnFire;
        }

        private void OnDisable()
        {
            _healthComponent.OnHealthDepleted -= ShipDestroyed;
            _healthComponent.OnHealthChanged -= HealthChanged;
            _moveComponent.OnMove -= AnimateMovement;
            _fireComponent.OnFire -= this.OnFire;
        }
        
        public void Fire()
        {
            if (_healthComponent.HasHealth)
            {
                _fireComponent.FireUp(Team);
            }
        }
        
        public void FireAt(Vector2 targetPosition)
        {
            if (!_healthComponent.HasHealth) return;

            var directionTarget = targetPosition - Position;
            _fireComponent.FireAt(Team,directionTarget);
        }

        public void SetDirection(Vector2? position) => _moveComponent.SetDirection(position);
        public void SetPosition(Vector2 position) => _moveComponent.SetPosition(position);

        public void TakeDamage(int damage)
        {
            OnDamageTaken?.Invoke();
            _healthComponent.ReceiveDamage(damage);
        }
        
        public void SetSpawner(BulletSpawner spawner)
        {
            _fireComponent.SetSpawner(spawner);
        }
        
        private void AnimateMovement(Vector2? direction, float speed)
        {
            OnMove?.Invoke(direction,speed);
        }

        private void ShipDestroyed()
        { 
            OnShipDestroyed?.Invoke(this.transform);
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