using System;
using Game.Scripts.GameObjects.Ship;
using Game.Scripts.Systems.Pool;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Scene
{
    public class SceneContextInstaller : MonoInstaller
    {
        [SerializeField] private Ship _ship;
        [SerializeField] private IMemoryPool<Bullet, BulletArgs> _bulletFactory;
        
        
        public override void InstallBindings()
        {
            this.Container.Bind<IShip>().To<Ship>().AsCached();
            this.Container.Bind<IMemoryPool<Bullet, BulletArgs>>().FromInstance(_bulletFactory);
        }
    }

    public struct BulletArgs
    {
    }

    [Serializable]
    public class SomeInstaller : Installer
    {
        [SerializeField] private Transform _transform;
        
        public override void InstallBindings()
        {
            
        }
    }
}