using System;
using Game.Scripts.GameObjects.Ship;
using Game.Scripts.Systems.Enemy.Spawn.ArgsProvider;
using UnityEngine;
using Zenject;

namespace Game
{
    public class ByTimeEnemyShipSpawner : EnemyShipSpawner, ITickable
    {
        private float _lastSpawnedTime = 0f;
        private readonly float _spawnCooldown;
        
        public ByTimeEnemyShipSpawner(EnemyCreateArgsProvider argsProvider,
            IMemoryPool<EnemyAI.Settings, EnemyAI> pool,EnemyEntity.Factory factory
            ,float spawnCooldown )
            : base (argsProvider, pool, factory)
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