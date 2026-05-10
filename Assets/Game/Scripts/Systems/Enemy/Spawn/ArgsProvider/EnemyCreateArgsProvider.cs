using System;
using UnityEngine;
using Zenject;

namespace Game
{
    [Serializable]
    public class EnemyCreateArgsProvider
    {
        private FirePointService _firePointService;
        private SpawnPointService _spawnPointService;
        private IShip _target;
        
        [Inject]
        public EnemyCreateArgsProvider(IShip target, FirePointService firePointService,
            SpawnPointService spawnPointService)
        {
            _target = target;
            _firePointService = firePointService;
            _spawnPointService = spawnPointService;
        }
        
        public EnemyShipAI.EnemyCreateArgs GetNewArgs()
        {
            var shipAIArgs = new EnemyShipAI.EnemyCreateArgs()
            {
                startPosition = _spawnPointService.GetSpawnPoint().Position,
                firePosition = _firePointService.GetFirePosition().Position,
                target = _target
            };
            
            return shipAIArgs;
        }
    }
}