using System.Collections.Generic;
using Modules.Utils;
using UnityEngine;

namespace Game
{
    public class ShipSpawner : MonoBehaviour
    {

        [SerializeField] private Ship _enemyShipPrefab;

        [SerializeField] private Transform[] _spawnPoints;

        [SerializeField] private Transform _container;

        private readonly Stack<Ship> _pool = new();

        public Ship SpawnEnemy()
        {
            if (_pool.TryPop(out Ship ship))
            {
                ship.gameObject.SetActive(true);
            }
            else
            {
                var spawnPosition = _spawnPoints.GetRandom().position;
                ship = Instantiate(_enemyShipPrefab, spawnPosition, Quaternion.identity);
                ship.OnShipDestroyed += DespawnEnemy;
            }
            
            return ship;
        }

        private void DespawnEnemy(Ship ship)
        {
            ship.gameObject.SetActive(false);
            ship.OnShipDestroyed -= DespawnEnemy;
            _pool.Push(ship);
        }
    }
}