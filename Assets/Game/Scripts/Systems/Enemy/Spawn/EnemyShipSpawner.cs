using System;
using System.Collections.Generic;
using Game.Scripts.GameObjects.Ship;
using Game.Scripts.Systems.Enemy.Spawn.ArgsProvider;
using UnityEngine;
using Zenject;

namespace Game
{
    public class EnemyShipSpawner
    {
        public event Action OnKillCountIncrease;
        private readonly EnemyCreateArgsProvider _argsProvider;
        private readonly IMemoryPool<EnemyAI.Settings, EnemyAI> _pool;
        private readonly EnemyEntity.Factory _factory;
        private List<EnemyAI> _spawnedShips;

        protected EnemyShipSpawner(EnemyCreateArgsProvider argsProvider,
            IMemoryPool<EnemyAI.Settings, EnemyAI> pool, EnemyEntity.Factory factory)
        {
            _argsProvider = argsProvider;
            _pool = pool;
            _factory = factory;
        }

        protected void Spawn()
        {
            var args = _argsProvider.GetNewArgs();
            var ship = _pool.Spawn(args);
            EnemyEntity enemy = _factory.Create(ship);
            ship.OnDispose += Despawn;
            _spawnedShips.Add(ship);
        }

        private void Despawn(EnemyAI ship)
        {
            ship.OnDispose -= Despawn;
            OnKillCountIncrease?.Invoke();
            _spawnedShips.Remove(ship);
        }

        public void StopAllShips()
        {
            foreach (var ship in _spawnedShips)
            {
                ship.SetTarget(null);
            }
        }
    }
}