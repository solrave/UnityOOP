using System;
using Game.Components;
using UnityEngine;
using Zenject;

namespace Game
{
    [Serializable]
    public sealed class Ship : IShip, IInitializable, IDisposable
    { 
        public event Action OnFire
        {
            add => _fireComponent.OnFire += value;
            remove => _fireComponent.OnFire -= value;
        }

        public event Action<Vector2?, float> OnMove
        {
            add => _moveComponent.OnMove += value;
            remove => _moveComponent.OnMove -= value;
        }  
        
        public event Action<int, int> OnHealthChanged 
        {
            add => _healthComponent.OnHealthChanged += value;
            remove => _healthComponent.OnHealthChanged -= value;
        }

        public event Action<Vector2> OnShipDestroyed;
        public event Action OnDamageTaken;
        
        public bool ReadyToShoot => _healthComponent.HasHealth;
        public Vector2 Position => _moveComponent.Position;
        
        [Inject]
        public TeamType Team {get;}
        
        [Inject]
        private IFireComponent _fireComponent;
        
        [Inject]
        private IHealthComponent _healthComponent;
        
        [Inject]
        private IMoveComponent _moveComponent;

        public void Initialize()
        {
            _healthComponent.OnHealthDepleted += ShipDestroyed;
        }

        public void Dispose()
        {
            _healthComponent.OnHealthDepleted -= ShipDestroyed;
        }
        
        public void Fire()
        {
            if (!_healthComponent.HasHealth) return;
            
                _fireComponent.FireUp(Team);
        }
        
        public void FireAt(Vector2 targetPosition)
        {
            if (!_healthComponent.HasHealth) return;

            var directionTarget = targetPosition - Position;
            _fireComponent.FireAt(Team, directionTarget.normalized);
        }

        public void SetDirection(Vector2? position) => _moveComponent.SetDirection(position);
        public void SetPosition(Vector2 position) => _moveComponent.SetPosition(position);

        public void TakeDamage(int damage)
        {
            OnDamageTaken?.Invoke();
            _healthComponent.ReceiveDamage(damage);
        }

        private void ShipDestroyed()
        { 
            OnShipDestroyed?.Invoke(_moveComponent.Position);
        }
    }
}