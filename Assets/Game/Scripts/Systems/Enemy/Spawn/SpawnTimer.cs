using UnityEngine;

namespace Game
{
    public class SpawnTimer
    {
        private float _lastSpawnedTime = 0f;
        private readonly float _spawnCooldown;
        
        public SpawnTimer(float spawnCooldown)
        {
            _spawnCooldown = spawnCooldown;
        }

        public bool TimeToSpawn(float time)
        {
            _lastSpawnedTime += time;
            
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