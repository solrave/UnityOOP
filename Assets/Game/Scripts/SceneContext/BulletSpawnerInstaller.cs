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
        
        public override void InstallBindings()
        {
            this.Container.Bind<BulletSpawner>()
                .AsSingle();
            
            this.Container.BindInterfacesAndSelfTo<BulletManager>()
                .AsSingle();
            
            this.Container
                .BindMemoryPool<Entity, Entity.Pool>()
                .WithId(ID.BulletPool)
                .FromComponentInNewPrefab(_bullet)
                .AsCached();
        }
    }
}