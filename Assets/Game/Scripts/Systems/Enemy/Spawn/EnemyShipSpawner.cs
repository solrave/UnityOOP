using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game
{
    public class EnemyShipSpawner
    {
        public event Action OnKillCountIncrease;
        
        private readonly EnemyCreateArgsProvider _argsProvider;
        private readonly IMemoryPool<EnemyShipAI.EnemyCreateArgs, EnemyShipAI> _pool;
        
        private List<EnemyShipAI> _spawnedShips;

        [Inject]
        public EnemyShipSpawner(EnemyCreateArgsProvider argsProvider,
            IMemoryPool<EnemyShipAI.EnemyCreateArgs, EnemyShipAI> pool)
        {
            _argsProvider = argsProvider;
            _pool = pool;
        }

        protected void Spawn()
        {
            var args = _argsProvider.GetNewArgs();
            var ship = _pool.Spawn(args);
            ship.OnDispose += Despawn;
            _spawnedShips.Add(ship);
        }

        private void Despawn(EnemyShipAI ship)
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
                ship.SetTarget(null);
            }
        }
    }
}