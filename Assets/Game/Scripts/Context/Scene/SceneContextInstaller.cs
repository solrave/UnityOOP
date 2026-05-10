using Game.Scripts.Scene;
using UnityEngine;
using Zenject;
using Game.Scripts.ZenjectExtensions;

namespace Game.Scripts.Context.Scene
{
    public class SceneContextInstaller : MonoInstaller
    {
        [SerializeField]
        private  ShipSpawnerInstaller _shipSpawnerInstaller;
        
        public override void InstallBindings()
        {
            this.Container.Install(_shipSpawnerInstaller);   
        }
    }
}