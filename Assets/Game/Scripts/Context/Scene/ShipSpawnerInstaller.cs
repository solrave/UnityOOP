using System;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Scene
{
    [Serializable]
    public class ShipSpawnerInstaller : Installer
    {
        [SerializeField]
        private EnemyCreateArgsProvider _argsProvider;

        [SerializeField]
        private FirePointService _firePointService;
        
        [SerializeField]
        private SpawnPointService _spawnPointService;

        [SerializeField] 
        private float _spawnCooldown;
        
        [SerializeField]
        private EnemyShipAI _enemyPrefab;
        
        public override void InstallBindings()
        {
            this.Container.Bind<EnemyCreateArgsProvider>().FromInstance(_argsProvider).AsSingle()
                .WithArguments(_firePointService, _spawnPointService);
            
            this.Container
                .BindMemoryPoolCustomInterface<EnemyShipAI, EnemyShipAI.Pool
                    ,IMemoryPool<EnemyShipAI.EnemyCreateArgs, EnemyShipAI>>()
                .FromInstance(_enemyPrefab);
            
            this.Container.Bind<ByTimeEnemyShipSpawner>()
                .AsSingle().WithArguments(_argsProvider, _spawnCooldown)
                .NonLazy();
        }
    }
}