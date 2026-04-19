using System;
using System.Collections.Generic;
using Game.Scripts.Systems.Pool;
using Modules.Utils;
using UnityEngine;

namespace Game
{
    public class ShipSpawner : MonoBehaviour
    {
        public event Action OnShipDespawned;

        [SerializeField] 
        private float _spawnCooldown;
        
        [SerializeField] 
        private EnemyShipAI _enemyShipPrefab;
        
        [SerializeField]
        private BulletSpawner _bulletSpawner;
        
        [SerializeField]
        private Ship _target;
        
        [SerializeField] 
        private Transform _container;
        
        [SerializeField]
        private FirePointsContainer _firePoints;

        [SerializeField]
        private SpawnPointsContainer _spawnPoints;
        
        private Pool<EnemyShipAI> _pool;
        
        private SpawnTimer _timer;

        private List<EnemyShipAI> _spawnedShips;
        
        private bool _spawnStopped;

        private void Awake()
        {
            _timer = new SpawnTimer(_spawnCooldown);
            _pool = new(_enemyShipPrefab, 8);
            _spawnedShips = new List<EnemyShipAI>();
        }
        
        private void Update()
        {
            if (_timer.TimeToSpawn(Time.deltaTime) && !_spawnStopped)
                Spawn();
        }

        private void Spawn()
        {
            var ship = _pool.Rent();
            ship.gameObject.SetActive(true);
            ship.SetPosition(_spawnPoints.GetSpawnPoint());
            ship.SetTarget(_target);
            ship.SetFirePosition(_firePoints.GetFirePosition());
            ship.OnShipDestroyed += Release;
            ship.SetSpawner(_bulletSpawner);
            _spawnedShips.Add(ship);
        }

        private void Release(EnemyShipAI shipAI)
        {
            OnShipDespawned?.Invoke();
            _spawnedShips.Remove(shipAI);
            _pool.Release(shipAI);
        }

        public void StopAllShips()
        {
            _spawnStopped = true;
            foreach (var ship in _spawnedShips)
            {
                ship.SetTarget(null);
                ship.SetTarget(null);
            }
        }
    }
}