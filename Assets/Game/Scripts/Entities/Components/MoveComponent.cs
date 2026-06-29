using System;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public interface IMoveComponent
    {
        public event Action<Vector2?, float> OnMove;
        bool IsMoving { get; }
        public void IncreaseSpeedBy(float amount);
        public void SetCondition(MoveComponent.ICondition condition);
        public void SetDirection(Vector2? direction);
    }
    
    public sealed class MoveComponent : IMoveComponent, IFixedTickable
    {
        [Serializable]
        public sealed class Settings
        {
            [field: SerializeField]
            public float Speed { get; private set; }
        }

        public interface ICondition
        {
            bool Evaluate();
        }
        
        public event Action<Vector2?, float> OnMove;
        public bool IsMoving => _inputDirection.HasValue;
        
        private ICondition _condition;
        private RigidbodyComponent _body;
        private readonly Settings _settings;
        private Vector2? _inputDirection;
        private Vector2 _simpleDirection;
        private float _speedMultiplier = 1;
        
        public MoveComponent(Settings settings, RigidbodyComponent body)
        {
            _settings = settings;
            _body = body;
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
                Vector2 newDirection = _body.Position + _inputDirection.Value * speedStep;
                _body.MovePosition(newDirection);
            }
            OnMove?.Invoke(_inputDirection, _settings.Speed);
        }
    }
}