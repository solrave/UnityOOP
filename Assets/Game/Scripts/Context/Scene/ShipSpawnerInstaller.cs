using System;
using Game.Scripts.Context.GameObject.Ship;
using Game.Scripts.GameObjects.Ship;
using Game.Scripts.Systems.Enemy.Spawn.Points;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Context.Scene
{
    [Serializable]
    public class ShipSpawnerInstaller : Installer
    {
        [SerializeField]
        private Entity _enemy;
        
        [SerializeField]
        private float _spawnCooldown;
        
        public override void InstallBindings()
        {
            this.Container.Bind<FirePointService>()
                .FromMethod(this.CreateFirePointService)
                .AsSingle().NonLazy();
            
            this.Container.Bind<SpawnPointService>()
                .FromMethod(this.CreateSpawnPointService)
                .AsSingle().NonLazy();

            // this.Container
            //     .BindMemoryPoolCustomInterface<EnemyAI, EnemyAI.Pool
            //         ,IMemoryPool<EnemyAI.Settings, EnemyAI>>()
            //     .WithInitialSize(4);

            this.Container
                .BindMemoryPool<Entity, Entity.Pool>()
                .FromComponentInNewPrefab(_enemy);
            
            // this.Container.BindFactory<EnemyAI, EnemyEntity, EnemyEntity.Factory>()
            //     .FromComponentInNewPrefab(_enemyEntity)
            //         .AsSingle();
            
            this.Container.BindInterfacesAndSelfTo<ByTimeEnemyShipSpawner>()
                .AsSingle().WithArguments(_spawnCooldown)
                .NonLazy();
            
            this.Container.Bind<EnemyShipSpawner>()
                .AsSingle();

        }

        private SpawnPointService CreateSpawnPointService()
        {
            SpawnPoint[] spawnPoints = UnityEngine.GameObject.FindObjectsByType<SpawnPoint>
                (FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            return new SpawnPointService(spawnPoints);
        }

        private FirePointService CreateFirePointService()
        {
            FirePoint[] firePoints = UnityEngine.GameObject.FindObjectsByType<FirePoint>
                (FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            return new FirePointService(firePoints);
        }
    }
}