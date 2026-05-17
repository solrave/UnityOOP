using Game.Scripts.Components;
using Game.Scripts.Components.Core;
using Game.Scripts.GameObjects.Ship;
using UnityEngine;
using Zenject;
using Game.Scripts.ZenjectExtensions;

namespace Game.Scripts.Context.GameObject.Ship.Enemy
{
    public class EnemyShipInstaller : BaseComponentsInstaller
    {
        [SerializeField] 
        private FollowComponent.Settings _followSetting;
        
        public override void InstallBindings()
        {
            base.InstallBindings();
            
            this.Container.BindInterfacesTo<IFollowComponent>()
                .AsSingle()
                .WithArguments(_followSetting);
            
            this.Container.Bind<EnemyShipComponents>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<EnemyAI>()
                .AsSingle()
                .NonLazy();
        }
    }
}