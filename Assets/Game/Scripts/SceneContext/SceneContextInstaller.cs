using UnityEngine;
using Zenject;
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
        private LevelBounds _bulletBounds;
        
        [SerializeField]
        private  ShipSpawnerInstaller _shipSpawnerInstaller;
        
        [SerializeField]
        private  BulletSpawnerInstaller _bulletSpawnerInstaller;
        
        public override void InstallBindings()
        {
            this.Container
                .Bind<PositionClamper>()
                .WithId(ID.PlayerPositionClamper)
                .AsCached()
                .WithArguments(_playerBounds);
            
            this.Container
                .Bind<PositionClamper>()
                .WithId(ID.BulletPositionClamper)
                .AsCached()
                .WithArguments(_bulletBounds);
            
            this.Container
                .BindInterfacesTo<PlayerController>()
                .AsSingle();

            this.Container.BindInterfacesTo<GameController>()
                .AsSingle();
            
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