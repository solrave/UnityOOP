using Game.Scripts.Context.GameObject.Ship.Player;
using UnityEngine;
using Zenject;
using Game.Scripts.ZenjectExtensions;
using Modules.Utils;

namespace Game.Scripts.Context.Scene
{
    public class SceneContextInstaller : MonoInstaller
    {
        [SerializeField] 
        private PlayerEntity _player;

        [SerializeField] 
        private LevelBounds _playerBounds;
        
        [SerializeField]
        private  ShipSpawnerInstaller _shipSpawnerInstaller;

        [SerializeField] 
        private BulletSpawnerInstaller _bulletSpawnerInstaller;
        
        public override void InstallBindings()
        {
            this.Container.BindInterfacesTo<PlayerController>()
                .AsSingle()
                .NonLazy();
            
            this.Container.BindInterfacesTo<PositionClamper>()
                .AsSingle()
                .NonLazy();
            
            this.Container.Bind<LevelBounds>()
                .FromInstance(_playerBounds)
                .AsSingle();
            
            this.Container.Bind<PlayerEntity>()
                .FromInstance(_player)
                .AsSingle();
            
            this.Container
                .Install(_shipSpawnerInstaller)
                .Install(_bulletSpawnerInstaller);   
        }
    }
}