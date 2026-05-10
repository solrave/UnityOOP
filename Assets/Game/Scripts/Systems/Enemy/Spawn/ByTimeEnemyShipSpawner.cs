using System;
using UnityEngine;
using Zenject;

namespace Game
{
    public class ByTimeEnemyShipSpawner : EnemyShipSpawner, ITickable
    {
        private float _lastSpawnedTime = 0f;
        private readonly float _spawnCooldown;
        
        public ByTimeEnemyShipSpawner(EnemyCreateArgsProvider argsProvider, EnemyShipAI.Pool pool
            ,float spawnCooldown ): base (argsProvider, pool)
        {
            _spawnCooldown = spawnCooldown;
        }
        
        public void Tick()
        {
            if (TimeToSpawn())
            {
                Spawn();
            }
        }

        private bool TimeToSpawn()
        {
            _lastSpawnedTime += Time.time;
            
            if (_lastSpawnedTime > _spawnCooldown)
            {
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