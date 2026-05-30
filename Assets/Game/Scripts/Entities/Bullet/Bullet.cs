using System;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class Bullet : IInitializable, IDisposable
    {
        public event Action OnHit;
        public event Action<Bullet> OnDispose;
        public TeamType Team => _teamComponent.team;
       
        private IMoveComponent _moveComponent;
        private RigidbodyComponent _bodyComponent;
        private CollisionListener _collisionListener;
        private DamageComponent _damageComponent;
        private TeamComponent _teamComponent;

        public Bullet(IMoveComponent moveComponent, RigidbodyComponent bodyComponent,
            CollisionListener collisionListener, TeamComponent teamComponent, DamageComponent damageComponent)
        {
            _moveComponent = moveComponent;
            _bodyComponent = bodyComponent;
            _collisionListener = collisionListener;
            _teamComponent = teamComponent;
            _damageComponent = damageComponent;
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

        public void IsExpired() => OnDispose?.Invoke(this);
        
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
            
                healthComponent.ReceiveDamage(_damageComponent);
                
            OnHit?.Invoke();
        }
    }
}