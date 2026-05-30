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
        private  ShipSpawnerInstaller _shipSpawnerInstaller;
        
        public override void InstallBindings()
        {
            this.Container
                .BindInterfacesTo<PositionClamper>()
                .AsSingle()
                .NonLazy();
            
            this.Container
                .BindInterfacesTo<PlayerController>()
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
                .Install(_shipSpawnerInstaller);   
        }
    }
}