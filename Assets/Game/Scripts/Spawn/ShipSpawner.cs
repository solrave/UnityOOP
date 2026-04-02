using System;
using System.Collections.Generic;
using Modules.Utils;
using UnityEngine;

namespace Game
{
    public class ShipSpawner : MonoBehaviour
    {

        public Action OnShipDespawned;
        
        [SerializeField]
        private Transform _target;
        [SerializeField]
        private EnemyShip _enemyShipPrefab;

        [SerializeField] 
        private Transform[] _spawnPoints;
        
        [SerializeField]
        private Transform[] _firePoints;
        
        [SerializeField]
        private BulletSpawner _bulletSpawner;
        
        
        [SerializeField] 
        private Transform _container;
        
        [SerializeField]
        private float _spawnCooldown;
        
        private readonly List<EnemyShip> _spawnedShips = new();
        private readonly Stack<EnemyShip> _pool = new();
        private float _lastSpawnedTime = 0f;
        private bool _spawnStopped = false;

        private void Update()
        {
            if (_lastSpawnedTime > _spawnCooldown && !_spawnStopped)
            { 
                Spawn();
                _lastSpawnedTime = 0f;
            }
            
            _lastSpawnedTime += Time.deltaTime;
        }
        
        private void Spawn()
        {
            Vector2 spawnPosition = _spawnPoints.GetRandom().position;
            _spawnPoints.Shuffle();
            Vector2 firePosition = _firePoints.GetRandom().position;
            _firePoints.Shuffle();
            
            if (_pool.TryPop(out EnemyShip ship))
            {
                ship.gameObject.SetActive(true);
                ship.GetComponent<Rigidbody2D>().position = spawnPosition;
                ship.transform.position = spawnPosition;
            }
            else
            {
                ship = Instantiate(_enemyShipPrefab, spawnPosition, Quaternion.identity);
                ship.SetTarget(_target);
                ship.OnShipDestroyed += DespawnEnemy;
                ship.SetSpawner(_bulletSpawner);
                Debug.Log($"ShipID: {ship.GetInstanceID()}");
            }
            
            _spawnedShips.Add(ship);
            ship.SetFollow(firePosition);
        }

        private void DespawnEnemy(Ship ship)
        {
            if (ship is not EnemyShip enemyShip) return;
            
            OnShipDespawned?.Invoke();
            _spawnedShips.Remove(enemyShip);
            _pool.Push(enemyShip);
            ship.gameObject.SetActive(false);
        }

        public void StopAllShips()
        {
            _spawnStopped = true;
            foreach (var ship in _spawnedShips)
            {
                ship.SetDestination(null);
                ship.SetTarget(null);
            }
        }
    }
}