using System;
using System.Collections.Generic;
using Modules.Utils;
using UnityEngine;

namespace Game
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private Transform _target;
        
        [SerializeField]
        private Transform[] _firePoints;
        
        [SerializeField]
        private float _spawnCooldown;

        [SerializeField] 
        private float _stoppingDistance;

        [SerializeField]
        private ShipSpawner _shipSpawner;

        [SerializeField]
        private BulletSpawner _bulletSpawner;

        private readonly List<Ship> _spawnedShips = new();
        private float _lastSpawnedTime;

        private void Awake()
        {
            
        }

        private void Update()
        {
            SpawnShipWithCooldown();
            MoveShips();
        }

        private void SpawnShipWithCooldown()
        {
            if (_lastSpawnedTime > _spawnCooldown)
            {
                SpawnShip();
                _lastSpawnedTime = 0f;
            }

            _lastSpawnedTime += Time.deltaTime;
        }

        private void SpawnShip()
        {
            var ship = _shipSpawner.SpawnEnemy();
            var destination = _firePoints.GetRandom().position;
            ship.SetDestination(destination);
            _spawnedShips.Add(ship);
        }

        private void MoveShips()
        {
            foreach (var ship in _spawnedShips)
            {
                Vector2 distance = ship.Destination - (Vector2) ship.transform.position;
                bool isNotReached = distance.sqrMagnitude > _stoppingDistance * _stoppingDistance;
            
                var moveDirection = isNotReached ? distance.normalized : Vector2.zero;

                if (isNotReached)
                {
                    ship.SetMoveDirection(moveDirection);
                }
                else
                {
                    ship.Fire();
                }
            }
        }
    }
}