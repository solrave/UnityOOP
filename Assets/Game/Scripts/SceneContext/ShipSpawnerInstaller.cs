using System;
using Game.Scripts.Context.GameObject.Ship;
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
                .FromComponentInNewPrefab(_enemy);
            
            this.Container.BindInterfacesAndSelfTo<ShipManager>()
                .AsSingle().WithArguments(_managerSettings)
                .NonLazy();
            
            this.Container.Bind<ShipSpawner>()
                .AsSingle();
        }

        private PointService CreatePointService()
        {
            Point[] spawnPoints = UnityEngine.GameObject.FindObjectsByType<Point>
                (FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            
            Point[] firePoints = UnityEngine.GameObject.FindObjectsByType<Point>
                (FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            
            return new PointService(spawnPoints, firePoints);
        }
    }
}