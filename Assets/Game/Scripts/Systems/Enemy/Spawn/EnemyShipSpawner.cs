using System;
using System.Collections.Generic;
using Game.Scripts.Context.GameObject.Ship;
using Game.Scripts.GameObjects.Ship;
using Game.Scripts.Systems.Enemy.Spawn.Points;
using UnityEngine;
using Zenject;

namespace Game
{
    public class EnemyShipSpawner
    {
        public event Action OnKillCountIncrease;
        private readonly FirePointService _firePointService;
        private readonly SpawnPointService _spawnPointService;
        private readonly Entity.Pool _pool;
        private List<Entity> _spawnedShips;

        protected EnemyShipSpawner(Entity.Pool pool)
        {
            _pool = pool;
        }

        protected void Spawn()
        {
            var startPosition = _spawnPointService.GetSpawnPoint().Position;
            var firePosition = _firePointService.GetFirePosition().Position;
            var ship = _pool.Spawn(startPosition,firePosition);
            _spawnedShips.Add(ship);
        }

        private void Despawn(Entity ship)
        {
            OnKillCountIncrease?.Invoke();
            _spawnedShips.Remove(ship);
        }

        public void StopAllShips()
        {
            foreach (var ship in _spawnedShips)
            {
                ship.Get<EnemyAI>().SetTarget(null);
            }
        }
    }
}