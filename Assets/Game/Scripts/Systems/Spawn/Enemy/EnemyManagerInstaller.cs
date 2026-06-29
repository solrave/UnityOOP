using System;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    [Serializable]
    public class EnemyManagerInstaller : Installer
    {
        [SerializeField]
        private Entity _enemy;
        
        [SerializeField]
        private EnemyManager.Settings _managerSettings;

        [SerializeField]
        private Transform _poolContainer;
        
        
        public override void InstallBindings()
        {
            this.Container.Bind<PointService>()
                .FromMethod(this.CreatePointService)
                .AsSingle().NonLazy();

            this.Container
                .BindMemoryPool<Entity, Entity.Pool>()
                .WithInitialSize(5)
                .FromComponentInNewPrefab(_enemy)
                .UnderTransform(_poolContainer)
                .AsCached()
                .WhenInjectedInto<EnemyManager>();
            
            this.Container.BindInterfacesAndSelfTo<EnemyManager>()
                .AsSingle()
                .WithArguments(_managerSettings)
                .NonLazy();
        }

        private PointService CreatePointService()
        {
            SpawnPoint[] spawnPoints = UnityEngine.GameObject.FindObjectsByType<SpawnPoint>
                (FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            
            FirePoint[] firePoints = UnityEngine.GameObject.FindObjectsByType<FirePoint>
                (FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            
            return new PointService(spawnPoints, firePoints);
        }
    }
}