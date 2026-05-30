using Game.Scripts.Context.GameObject.Ship;
using UnityEngine;
using Zenject;

namespace Game
{
    public class ByTimeEnemyShipSpawner : EnemyShipSpawner, ITickable
    {
        private float _lastSpawnedTime = 0f;
        private readonly float _spawnCooldown;
        
        protected ByTimeEnemyShipSpawner(Entity.Pool pool, FirePointService firePointService
            ,PointService pointService, float spawnCooldown)
            : base(pool, firePointService, pointService)
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
            if (_lastSpawnedTime >= _spawnCooldown)
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