using System;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class Bullet : IDisposable
    {
        public event Action<Entity> OnHit;
        public event Action OnExplode;
        public TeamType Team => _teamComponent.team;
       
        private IMoveComponent _moveComponent;
        private RigidbodyComponent _bodyComponent;
        private CollisionListener _collisionListener;
        private DamageComponent _damageComponent;
        private TeamComponent _teamComponent;
        private Entity _entity;

        public Bullet(IMoveComponent moveComponent, RigidbodyComponent bodyComponent,
            CollisionListener collisionListener, TeamComponent teamComponent,
            DamageComponent damageComponent, Entity entity)
        {
            _moveComponent = moveComponent;
            _bodyComponent = bodyComponent;
            _collisionListener = collisionListener;
            _teamComponent = teamComponent;
            _damageComponent = damageComponent;
            _entity = entity;
        }

        public void Initialize()
        {
            SetLayer(_teamComponent.team);
            _collisionListener.OnCollision += OnCollision;
        }

        public void Dispose()
        {
           SetLayer(TeamType.None);
            _collisionListener.OnCollision -= OnCollision;
        }
        
        private void SetLayer(TeamType team)
        {
            _bodyComponent.Layer = team switch
            {
                TeamType.None => LayerMask.NameToLayer("Default"),
                TeamType.Player => LayerMask.NameToLayer("PlayerBullet"),
                TeamType.Enemy => LayerMask.NameToLayer("EnemyBullet"),
                _ => throw new ArgumentOutOfRangeException(nameof(team), team, null)
            };
        }
        
        private void OnCollision(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out IEntity entity)
                && entity.Get<TeamComponent>().team != this._teamComponent.team &&
                entity.TryGet<IHealthComponent>(out var healthComponent))
            {
                healthComponent.ReceiveDamage(_damageComponent);
                Debug.Log($"BulletEntity: {_entity.Name}");
                OnExplode?.Invoke();
                OnHit?.Invoke(_entity);
            }
        }
    }
}