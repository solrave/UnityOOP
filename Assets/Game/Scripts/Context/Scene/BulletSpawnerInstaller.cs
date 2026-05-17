using System;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Context.Scene
{
    [Serializable]
    public class BulletSpawnerInstaller : Installer
    {
        [SerializeField]
        private Bullet _bulletPrefab;
        
        public override void InstallBindings()
        {
            this.Container.BindMemoryPoolCustomInterface<Bullet, Bullet.Pool
                ,IMemoryPool<Bullet.BulletCreateArgs, Bullet>>().FromInstance(_bulletPrefab);
            
        }
    }
}