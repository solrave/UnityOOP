using System;
using Game.Scripts.Components;
using Game.Scripts.Systems.Pool;
using UnityEngine;

namespace Game
{
    public class Bullet : MonoBehaviour, ISpawnableBullet
    {
        public event Action OnHit;
        public event Action<Bullet> OnExpired;
        
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

        public void IsExpired() => OnExpired?.Invoke(this);

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
            
            OnHit?.Invoke();
        }

        public void Setup(TeamType team, Vector2 position)
        {
            Team = team;
            transform.position = position;
        }
    }
}