using System;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class ShipManager : ITickable
    {
        [Serializable]
        public class Settings
        {
            [SerializeField] public float spawnCooldown;
        }
        
        private float _lastSpawnedTime = 0f;
        private readonly Settings _settings;
        private readonly ShipSpawner _shipSpawner;
        
        protected ShipManager(Settings settings, ShipSpawner shipSpawner)
        {
            _settings = settings;
            _shipSpawner = shipSpawner;
        }

        public void Tick()
        {
            if (TimeToSpawn())
                _shipSpawner.Spawn();
        }

        private bool TimeToSpawn()
        {
            _lastSpawnedTime += Time.deltaTime;
            if (_lastSpawnedTime >= _settings.spawnCooldown)
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

