using System;
using Modules.Utils;
using UnityEngine;

namespace Game
{
    [Serializable]
    public sealed class MoveComponent
    {
        public event Action<Vector2?, float> OnMove;  
        
        [SerializeField]
        private Rigidbody2D _rigidbody;
        
        [SerializeField]
        private float _speed = 5f;
        
        private Vector2? _inputDirection;
        private Vector2 _simpleDirection;

        public void SetSpeed(float speed) => _speed = speed;

        public void SetDirection(Vector2? direction) => _inputDirection = direction;
        public void SetPosition(Vector2 position) => _rigidbody.position = position;

        public void FixedUpdate() => Move();
        
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
}