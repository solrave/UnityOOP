using System;
using Modules.Utils;
using UnityEngine;

namespace Game
{
    // +
    public sealed class MoveComponent : MonoBehaviour
    {
        [SerializeField]
        private Transform _visualTransform;
        
        [SerializeField]
        private Rigidbody2D _rigidbody;
        
        [SerializeField]
        private float _speed = 5f;
        
        [SerializeField]
        private float _moveRotationAngle  = 30f;
        
        private Vector2? _inputDirection;
        private Vector2 _simpleDirection;

        public void SetSpeed(float speed) => _speed = speed;

        public void SetDirection(Vector2? direction) => _inputDirection = direction;

        private void FixedUpdate() => Move();
        
        private void Move()
        {
            if (_inputDirection.HasValue)
            {
                Vector2 newDirection = _rigidbody.position + _inputDirection.Value * (_speed * Time.fixedDeltaTime);
                _rigidbody.MovePosition(newDirection);
                _inputDirection = null;
            }
            TiltTransform();
        }
        
        private void TiltTransform()
        {
            Vector3 shipAngles = _visualTransform.localEulerAngles;
            
            if (_inputDirection is not null)
            {
                shipAngles.x = _moveRotationAngle * _inputDirection.Value.y;
                shipAngles.y = _moveRotationAngle / 2 * _inputDirection.Value.x * -1f;
            }
            
            Quaternion shipRotation = Quaternion.Euler(shipAngles);
            float t = _speed * Time.deltaTime;
            _visualTransform.localRotation = Quaternion.Lerp(_visualTransform.localRotation, shipRotation, t);
        }
    }
}