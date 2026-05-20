using Game.Scripts.Context.GameObject.Ship;
using UnityEngine;
using Zenject;

namespace Game
{
    public class ByTimeEnemyShipSpawner : EnemyShipSpawner, ITickable
    {
        private float _lastSpawnedTime = 0f;
        private readonly float _spawnCooldown;
        
        public ByTimeEnemyShipSpawner(Entity.Pool pool,float spawnCooldown)
            : base (pool)
        {
            _spawnCooldown = spawnCooldown;
        }
        
        public void Tick()
        {
            if (TimeToSpawn())
                Spawn();
        }

        private bool TimeToSpawn()
        {
            _lastSpawnedTime += Time.deltaTime;
            Debug.Log($"{_lastSpawnedTime}");
            if (_lastSpawnedTime > _spawnCooldown)
            {
                Debug.Log($"SPAWN!");
                ResetTimer();
                return true;
            }

            return false;
        }

        private void ResetTimer()
        {
            _lastSpawnedTime = 0f;
        }
    }
}