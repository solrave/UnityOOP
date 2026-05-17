using System;
using Game.Scripts.ZenjectExtensions;
using UnityEngine;

namespace Game
{
    public class SpawnPointService
    {
        private SpawnPoint[] _spawnPoints;

        public SpawnPointService(SpawnPoint[] spawnPoints)
        {
            _spawnPoints = spawnPoints;
        }
        
        public IPoint GetSpawnPoint()
        {
            _spawnPoints.Shuffle();
            return _spawnPoints.GetRandom();
        }
    }
}