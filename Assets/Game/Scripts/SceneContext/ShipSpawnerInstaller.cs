using System;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    [Serializable]
    public class ShipSpawnerInstaller : Installer
    {
        [SerializeField]
        private Entity _enemy;
        
        [SerializeField]
        private ShipManager.Settings _managerSettings;
        
        public override void InstallBindings()
        {
            this.Container.Bind<PointService>()
                .FromMethod(this.CreatePointService)
                .AsSingle().NonLazy();
            
            this.Container
                .BindMemoryPool<Entity, Entity.Pool>()
                .FromComponentInNewPrefab(_enemy)
                .AsCached();
            
            this.Container.BindInterfacesAndSelfTo<ShipManager>()
                .AsSingle().WithArguments(_managerSettings)
                .NonLazy();
            
            this.Container.Bind<ShipSpawner>()
                .AsSingle();
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