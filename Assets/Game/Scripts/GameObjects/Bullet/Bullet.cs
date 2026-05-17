using System;
using Game.Scripts.Components;
using Game.Scripts.Components.Core;
using Game.Scripts.Systems.Pool;
using UnityEngine;
using Zenject;

namespace Game
{
    public class Bullet : MonoBehaviour, IInitializable, IDisposable
    {
        public sealed class Pool : MemoryPool<BulletCreateArgs, Bullet>
        {
            protected override void Reinitialize(BulletCreateArgs args, Bullet enemy)
            {
                enemy.SetTeam(args.team);
                enemy.SetPosition(args.position);
                enemy.SetDirection(args.direction);
            }
            
            protected override void OnSpawned(Bullet bullet)
            {
                base.OnSpawned(bullet);
                bullet.OnDispose += this.Despawn;
            }

            protected override void OnDespawned(Bullet bullet)
            {
                bullet.OnDispose -= this.Despawn;
                base.OnDespawned(bullet);
            }
        }
        
        public struct BulletCreateArgs
        {
            public TeamType team;
            public Vector2 position;
            public Vector2 direction;
        }
        
        public event Action OnHit;
        public event Action<Bullet> OnDispose;
        
        public TeamType Team { get; private set; }
        public Vector2 Position => this.transform.position;
        private MoveComponent _moveComponent;
        private int _damage;
        
        [Inject] 
        public void Construct(MoveComponent moveComponent, int damage)
        {
            _moveComponent = moveComponent;
            _damage = damage;
        }

        public void SetDirection(Vector2? direction) => _moveComponent.SetDirection(direction);
        public void SetTeam(TeamType type) => Team = type;

        public void SetPosition(Vector2 position) => this.transform.position = position;

        public void IsExpired() => OnDispose?.Invoke(this);
        
        public void FixedUpdate()  => _moveComponent.FixedTick();

        public void Initialize()
        {
            SetLayer(Team);
        }

        public void Dispose()
        {
           SetLayer(TeamType.None);
        }
        
        private void SetLayer(TeamType team)
        {
            this.gameObject.layer = team switch
            {
                TeamType.None => LayerMask.NameToLayer("Default"),
                TeamType.Player => LayerMask.NameToLayer("PlayerBullet"),
                TeamType.Enemy => LayerMask.NameToLayer("EnemyBullet"),
                _ => throw new ArgumentOutOfRangeException(nameof(team), team, null)
            };
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IGameEntity entity) 
                && entity.TryGet<IHealthComponent>(out var healthComponent))
            
                healthComponent.ReceiveDamage(_damage);
                
            OnHit?.Invoke();
        }
    }
}