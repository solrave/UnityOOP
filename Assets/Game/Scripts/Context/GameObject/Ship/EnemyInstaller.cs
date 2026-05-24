using Game.Scripts.Components.Core;
using Game.Scripts.GameObjects.Ship;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Context.GameObject.Ship
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField]
        private FollowComponent.Settings _followSettings;
        
        public override void InstallBindings()
        {
            Container
                .Bind<Entity>()
                .FromComponentInHierarchy()
                .AsSingle().NonLazy();
                
            this.Container
                .Bind<EnemyAI>()
                .AsSingle();
            
            this.Container
                .BindInterfacesTo<FollowComponent>()
                .AsSingle()
                .WithArguments(_followSettings);
        }
    }
}