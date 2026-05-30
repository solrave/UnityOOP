using System;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public sealed class MoveComponent : IMoveComponent, IFixedTickable
    {
        [Serializable]
        public sealed class Settings
        {
            [field: SerializeField]
            public Rigidbody2D Rigidbody { get; private set; }
            
            [field: SerializeField]
            public float Speed { get; private set; }
        }

        public interface ICondition
        {
            bool Evaluate();
        }
        
        public event Action<Vector2?, float> OnMove;
        public Vector2 Position => _settings.Rigidbody.position;
        public bool IsMoving => _inputDirection.HasValue;
        
        private readonly Settings _settings;
        private ICondition _condition;
        
        private Vector2? _inputDirection;
        private Vector2 _simpleDirection;
        private float _speedMultiplier = 1;
        
        public MoveComponent(Settings settings)
        {
            _settings = settings;
        }

        public void IncreaseSpeedBy(float amount) => _speedMultiplier = amount;
        public void SetDirection(Vector2? direction) => _inputDirection = direction;
        public void SetCondition(ICondition condition) => _condition = condition;

        public void FixedTick() => Move();
        
        private void Move()
        {
            if (_inputDirection.HasValue)
            {
                var speedStep = _settings.Speed * _speedMultiplier * Time.fixedDeltaTime;
                Vector2 newDirection = _settings.Rigidbody.position + _inputDirection.Value * speedStep;
                _settings.Rigidbody.MovePosition(newDirection);
            }
            OnMove?.Invoke(_inputDirection, _settings.Speed);
        }
    }

    public interface IMoveComponent
    {
        public event Action<Vector2?, float> OnMove;
        Vector2 Position { get;}
        bool IsMoving { get; }
        public void IncreaseSpeedBy(float amount);
        public void SetCondition(MoveComponent.ICondition condition);
        public void SetDirection(Vector2? direction);
    }
}