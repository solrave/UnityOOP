using System;
using Modules.Utils;
using UnityEngine;
using Zenject;

namespace Game
{
    [Serializable]
    public sealed class MoveComponent : IMoveComponent, IFixedTickable
    {
        public event Action<Vector2?, float> OnMove;
        public Vector2 Position => _rigidbody.position;

        private Rigidbody2D _rigidbody;
        private float _speed;
        
        private Vector2? _inputDirection;
        private Vector2 _simpleDirection;

        [Inject]
        public MoveComponent(Rigidbody2D rigidbody, float speed)
        {
            _rigidbody = rigidbody;
            _speed = speed;
        }

        public void SetSpeed(float speed) => _speed = speed;
        public void SetDirection(Vector2? direction) => _inputDirection = direction;
        public void SetPosition(Vector2 position) => _rigidbody.position = position;

        public void FixedTick() => Move();
        
        private void Move()
        {
            if (_inputDirection.HasValue)
            {
                Vector2 newDirection = _rigidbody.position + _inputDirection.Value * (_speed * Time.fixedDeltaTime);
                _rigidbody.MovePosition(newDirection);
            }
            OnMove?.Invoke(_inputDirection, _speed);
        }
    }

    public interface IMoveComponent
    {
        public event Action<Vector2?, float> OnMove;
        Vector2 Position { get;}
        public void SetSpeed(float speed);
        public void SetDirection(Vector2? direction);
        public void SetPosition(Vector2 position);
    }
}