using System;
using Zenject;

namespace Game.Gameplay
{
    [Serializable]
    public class BulletSpawnerInstaller : Installer
    {
        public override void InstallBindings()
        {
            this.Container.Bind<BulletSpawner>()
                .AsSingle();
            
            this.Container.BindInterfacesAndSelfTo<BulletManager>()
                .AsSingle();
        }
    }
}