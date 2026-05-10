using System;
using Game.Scripts.ZenjectExtensions;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class SpawnPointService
    {
        [SerializeField] 
        private SpawnPoint[] _spawnPoints;

        public IPoint GetSpawnPoint()
        {
            _spawnPoints.Shuffle();
            return _spawnPoints.GetRandom();
        }
    }
}