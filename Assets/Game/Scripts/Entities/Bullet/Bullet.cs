using System;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class Bullet : IInitializable, IDisposable
    {
        public event Action OnHit;
        public event Action OnInitialized;
       
        private IMoveComponent _moveComponent;
        private readonly RigidbodyComponent _bodyComponent;
        private readonly CollisionObservable _collisionObservable;
        private readonly DamageComponent _damageComponent;
        private readonly TeamComponent _teamComponent;

        public Bullet(IMoveComponent moveComponent,
            RigidbodyComponent bodyComponent,
            CollisionObservable collisionObservable,
            TeamComponent teamComponent,
            DamageComponent damageComponent)
        {
            _moveComponent = moveComponent;
            _bodyComponent = bodyComponent;
            _collisionObservable = collisionObservable;
            _teamComponent = teamComponent;
            _damageComponent = damageComponent;
        }

        public void Initialize()
        {
            _teamComponent.OnTeamChanged += SetLayer;
            _collisionObservable.OnCollision += OnCollision;
            OnInitialized?.Invoke();
        }

        public void Dispose()
        {
           SetLayer(TeamType.None);
            _teamComponent.OnTeamChanged -= SetLayer;
            _collisionObservable.OnCollision -= OnCollision;
        }

        public void ApplyHit() => OnHit?.Invoke();
        
        private void SetLayer(TeamType team)
        {
            _bodyComponent.Layer = team switch
            {
                TeamType.None => LayerMask.NameToLayer("Default"),
                TeamType.Player => LayerMask.NameToLayer("Player"),
                TeamType.Enemy => LayerMask.NameToLayer("Enemy"),
                _ => throw new ArgumentOutOfRangeException(nameof(team), team, null)
            };
        }
        
        private void OnCollision(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out IEntity entity)
                && entity.Get<TeamComponent>().Team != this._teamComponent.Team
                && entity.TryGet<IHealthComponent>(out var healthComponent))
            {
                healthComponent.ReceiveDamage(_damageComponent);
                OnHit?.Invoke(); 
            }
        }
    }
}