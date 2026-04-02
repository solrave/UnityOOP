using System;
using Game.Scripts.Components;
using UnityEngine;

namespace Game
{
    public class Bullet : MonoBehaviour
    {
        public event Action<Bullet> OnHit;
        
        [field: SerializeField]
        public TeamType Team { get; private set; }

        [SerializeField] 
        private MoveComponent _moveComponent;
        
        [SerializeField] 
        private BulletAnimationComponent _animationComponent;

        [SerializeField] 
        private int _damage;

        private Vector2? _direction;

        public void SetDirection(Vector2? direction) => _direction = direction;

        private void OnEnable()
        {
            SetLayer();
            OnHit += _animationComponent.PlayExplosion;
            _animationComponent.PlayVisual();
        }

        private void SetLayer()
        {
            this.gameObject.layer = Team switch
            {
                TeamType.None => LayerMask.NameToLayer("Default"),
                TeamType.Player => LayerMask.NameToLayer("PlayerBullet"),
                TeamType.Enemy => LayerMask.NameToLayer("EnemyBullet"),
                _ => throw new ArgumentOutOfRangeException(nameof(this.Team), this.Team, null)
            };
        }

        private void OnDisable()
        {
            OnHit -= _animationComponent.PlayExplosion;
        }

        private void FixedUpdate()
        {
            _moveComponent.SetDirection(_direction);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out IDamageable ship))
                return;
            
            if (_damage > 0)
            {
                ship.TakeDamage(_damage);
            }
            
            OnHit?.Invoke(this);
            _animationComponent.StopVisual();
            this.gameObject.SetActive(false);

        }
    }
}