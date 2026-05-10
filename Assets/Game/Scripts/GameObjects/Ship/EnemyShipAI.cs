using System;
using Game.Scripts.GameObjects.Components;
using UnityEngine;
using Zenject;

namespace Game
{
    [Serializable]
    public class EnemyShipAI : IInitializable, IDisposable, IFixedTickable
    {
        public sealed class Pool : MemoryPool<EnemyCreateArgs, EnemyShipAI>
        {
            private readonly TickableManager _tickableManager;

            public Pool(TickableManager tickableManager)
            {
                _tickableManager = tickableManager;
            }
            
            protected override void Reinitialize(EnemyCreateArgs args, EnemyShipAI bullet)
            {
                bullet.SetPosition(args.startPosition);
                bullet.SetFirePosition(args.firePosition);
                bullet.SetTarget(args.target);
            }
            
            protected override void OnSpawned(EnemyShipAI shipAI)
            {
                base.OnSpawned(shipAI);
                shipAI.OnDispose += this.Despawn;
                _tickableManager.AddFixed(shipAI);
            }

            protected override void OnDespawned(EnemyShipAI shipAI)
            {
                _tickableManager.RemoveFixed(shipAI);
                shipAI.OnDispose -= this.Despawn;
                base.OnDespawned(shipAI);
            }
        }

        public struct EnemyCreateArgs
        {
            public Vector2 startPosition;
            public Vector2 firePosition;
            public IShip target;
        }
        
        public event Action<EnemyShipAI> OnDispose;
        
        private IShip _enemyShip;
        private FollowComponent _followComponent;
        private Vector2 _firePosition;
        private IShip _target;

        [Inject]
        public EnemyShipAI( IShip enemyShip, FollowComponent followComponent)
        {
            _enemyShip = enemyShip;
            _followComponent = followComponent;
        }
        
        public void Initialize()
        {
            _enemyShip.OnShipDestroyed += AIShipDestroyed;
        }

        public void Dispose()
        {
            _enemyShip.OnShipDestroyed -= AIShipDestroyed;
        }
        
        public void FixedTick()
        {
            var targetDirection = _followComponent.GetDirection(_firePosition, _enemyShip.Position);
            _enemyShip.SetDirection(targetDirection);
            
            if (_followComponent.IsReached && _enemyShip.ReadyToShoot && _target != null)
            {
                _enemyShip.FireAt(_target.Position);
            }
        }
        
        public void SetPosition(Vector2 position) => _enemyShip.SetPosition(position);
        public void SetFirePosition(Vector2 position) => _firePosition = position;
        public void SetTarget(IShip ship) => _target = ship;

        private void AIShipDestroyed(Vector2 obj)
        {
            OnDispose?.Invoke(this);
        }
    }
}
