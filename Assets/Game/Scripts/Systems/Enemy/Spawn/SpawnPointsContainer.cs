using System;
using Modules.Utils;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class SpawnPointsContainer
    {
        [SerializeField] 
        private Transform[] _spawnPoints;

        public Vector2 GetSpawnPoint()
        {
            _spawnPoints.Shuffle();
            Vector2 spawnPosition = _spawnPoints.GetRandom().position;
            return spawnPosition;
        }
    }
}