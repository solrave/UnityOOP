using System;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    [Serializable]
    public class BulletSpawnerInstaller : Installer
    {
        [SerializeField]
        private Entity _bullet;
        
        [SerializeField]
        private Transform _poolContainer;
        
        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<BulletManager>()
                .AsSingle();
            
            this.Container
                .BindMemoryPool<Entity, Entity.Pool>()
                .WithInitialSize(5)
                .FromComponentInNewPrefab(_bullet)
                .UnderTransform(_poolContainer)
                .AsCached()
                .WhenInjectedInto<BulletManager>();
        }
    }
}