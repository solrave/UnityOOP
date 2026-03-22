using System;
using Game.Components;
using Game.Scripts.Components;
using UnityEngine;

namespace Game
{
    public class Ship : MonoBehaviour
    {
        public event Action OnDamageTaken;
        public event Action<Ship> OnShipDestroyed;
        public event Action<Transform, TeamType> OnFire;

        public bool HasHealth => _healthComponent.HasHealth;
        public Vector2 Destination => _destination;

        [SerializeField]
        private FireComponent _fireComponent;
        
        [SerializeField]
        private HealthComponent _healthComponent;
        
        [SerializeField]
        private MoveComponent _moveComponent;
        
        [SerializeField]
        private ShipAnimationComponent shipAnimationComponent;

        private Vector2 _destination;
        
        private void OnEnable()
        {
            _fireComponent.FireAnimationRequested += shipAnimationComponent.AnimateFire;
            this.OnDamageTaken += shipAnimationComponent.AnimateDamage;
            this.OnShipDestroyed += shipAnimationComponent.AnimateDestruction;
        }

        private void OnDisable()
        {
            _fireComponent.FireAnimationRequested -= shipAnimationComponent.AnimateFire;
            this.OnDamageTaken -= shipAnimationComponent.AnimateDamage;
            this.OnShipDestroyed -= shipAnimationComponent.AnimateDestruction;
        }

        protected virtual void FixedUpdate() => _moveComponent.Move();

        public void Fire()
        {
            if (_healthComponent.HasHealth)
            {
                _fireComponent.Fire();
                this.OnFire?.Invoke(_fireComponent.FirePoint, _fireComponent.Team);
            }
        }

        public Vector2 SetDestination(Vector2 destination) => _destination = destination;

        public void SetMoveDirection(Vector2? position)
        {
            _moveComponent.SetInputDirection(position);
        }

        public void TakeDamage(int damage)
        {
            _healthComponent.ReceiveDamage(damage);
            
            if (_healthComponent.HasHealth)
            {
                this.OnDamageTaken?.Invoke();
            }
            else
            {
                this.OnShipDestroyed?.Invoke(this);
                this.gameObject.SetActive(false);
            }
        }
    }
}