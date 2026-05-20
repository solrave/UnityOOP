using System;
using Game.Scripts.Components.Core;
using Game.Scripts.Context.GameObject.Ship;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameObjects.Ship
{
    public class EnemyAI : IInitializable, IDisposable, IFixedTickable
    {
        public struct Settings
        {
            public Vector2 startPosition;
            public Vector2 firePosition;
        }
        
        public sealed class Pool : MemoryPool<Settings, EnemyAI>
        {
            private readonly TickableManager _tickableManager;

            public Pool(TickableManager tickableManager)
            {
                _tickableManager = tickableManager;
            }
            
            protected override void Reinitialize(Settings set, EnemyAI enemy)
            {
                enemy.SetPosition(set.startPosition);
                enemy.SetFirePosition(set.firePosition);
            }
            
            protected override void OnSpawned(EnemyAI ai)
            {
                base.OnSpawned(ai);
                ai.Initialize();
                ai.OnDispose += this.Despawn;
                _tickableManager.AddFixed(ai);
            }

            protected override void OnDespawned(EnemyAI ai)
            {
                _tickableManager.RemoveFixed(ai);
                ai.OnDispose -= this.Despawn;
                ai.Dispose();
                base.OnDespawned(ai);
            }
        }

        public event Action<EnemyAI> OnDispose;
        
        private Entity _enemy;
        private Entity _target;
        private Vector2 _firePosition;
        
        public EnemyAI()
        {
            
        }
        
        public void Initialize()
        {
            _enemy.Get<IHealthComponent>().OnHealthDepleted += EnemyShipDestroyed;
        }

        public void Dispose()
        {
            _enemy.Get<IHealthComponent>().OnHealthDepleted -= EnemyShipDestroyed;
        }
        
        public void FixedTick()
        {
            var targetDirection = _enemy.Get<IFollowComponent>()
                .GetDirection(_firePosition, _enemy.Get<IMoveComponent>().Position);
            _enemy.Get<IMoveComponent>().SetDirection(targetDirection);
            
            if (_enemy.Get<IFollowComponent>().IsReached && _enemy.Get<IFireComponent>().ReadyToShoot && _target != null)
            {
                _enemy.Get<IFireComponent>().FireAt(_target.Get<IMoveComponent>().Position);
            }
        }
        
        public void SetPosition(Vector2 position) => _enemy.Get<IMoveComponent>().SetPosition(position);
        public void SetFirePosition(Vector2 position) => _firePosition = position;
        public void SetTarget(Entity playerEntity) => _target = playerEntity;

        private void EnemyShipDestroyed()
        {
            OnDispose?.Invoke(this);
        }
    }
}
