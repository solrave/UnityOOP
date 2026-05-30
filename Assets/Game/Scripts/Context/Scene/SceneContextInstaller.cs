using Game.Scripts.Context.GameObject.Ship;
using Game.Scripts.GameObjects.Ship;
using UnityEngine;
using Zenject;
using Game.Scripts.ZenjectExtensions;
using Modules.Utils;

namespace Game.Gameplay
{
    public class SceneContextInstaller : MonoInstaller
    {
        [SerializeField] 
        private Entity _player;
        
        [SerializeField] 
        private Entity _enemy;

        [SerializeField] 
        private LevelBounds _playerBounds;
        
        [SerializeField]
        private  ShipSpawnerInstaller _shipSpawnerInstaller;

        [SerializeField] 
        private BulletSpawnerInstaller _bulletSpawnerInstaller;
        
        public override void InstallBindings()
        {
            this.Container
                .BindInterfacesTo<PlayerController>()
                .AsSingle()
                .NonLazy();
            
            this.Container
                .BindInterfacesTo<PlayerClamper>()
                .AsSingle()
                .NonLazy();
            
            this.Container
                .BindInterfacesTo<BulletClamper>()
                .AsSingle()
                .NonLazy();
            
            this.Container
                .Bind<LevelBounds>()
                .FromInstance(_playerBounds)
                .AsCached();
            
            this.Container
                .Bind<Entity>()
                .FromInstance(_player)
                .AsSingle();
            
            this.Container
                .Bind<CharacterProvider>()
                .AsSingle();
            
            this.Container
                .Install(_shipSpawnerInstaller)
                .Install(_bulletSpawnerInstaller);   
        }
    }
}