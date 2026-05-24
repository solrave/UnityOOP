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
        private readonly List<Entity> _spawnedShips = new List<Entity>();

        protected EnemyShipSpawner(Entity.Pool pool, FirePointService firePointService
            , SpawnPointService spawnPointService)
        {
            _pool = pool;
            _firePointService = firePointService;
            _spawnPointService = spawnPointService;
        }

        protected void Spawn()
        {
            var startPosition = _spawnPointService.GetSpawnPoint().Position;
            var firePosition = _firePointService.GetFirePosition().Position;
            var ship = _pool.Spawn(startPosition,firePosition);
            Debug.Log($"SPAWNED SHIP NOT NULL:  {ship is not null}");
            _spawnedShips.Add(ship);
            //ship.Run();
        }

        private void Despawn(Entity ship)
        {
            OnKillCountIncrease?.Invoke();
            _spawnedShips.Remove(ship);
            _pool.Despawn(ship);
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