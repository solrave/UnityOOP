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

        public Vector2 Position => this.transform.position;

        [SerializeField] 
        private MoveComponent _moveComponent;

        [SerializeField] 
        private int _damage;

        public void SetDirection(Vector2? direction) => _moveComponent.SetDirection(direction);
        public void SetTeam(TeamType type) => Team = type;

        public void SetPosition(Vector2 position) => this.transform.position = position;

        private void FixedUpdate() => _moveComponent.FixedUpdate();

        private void OnEnable()
        {
            SetLayer();
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

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out IDamageable ship))
                return;
            
            if (_damage > 0)
            {
                ship.TakeDamage(_damage);
            }
            
            OnHit?.Invoke(this);
            this.gameObject.SetActive(false);
        }
    }
}