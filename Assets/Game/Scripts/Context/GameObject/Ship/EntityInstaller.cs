using System;
using Game.Scripts.Components;
using Game.Scripts.Components.Core;
using Game.Scripts.GameObjects.Ship;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Context.GameObject.Ship
{
    [Serializable]
    public class EntityInstaller : MonoInstaller
    {
        [SerializeField] private Entity _sceneContext;
        
        [Header("Settings")]
        [SerializeField] 
        private FireComponent.Settings _fireSettings;
        
        [SerializeField] 
        private MoveComponent.Settings  _moveSettings;
        
        [SerializeField]
        private HealthComponent.Settings _healthSettings;

        [SerializeField]
        private FollowComponent.Settings _followSettings;

        public override void InstallBindings()
        {
            this.Container.Bind<ShipComponents>()
                .AsSingle()
                .NonLazy();
            
            this.Container.Bind<Entity>()
                .FromInstance(_sceneContext)
                .AsSingle()
                .WhenInjectedInto<ShipView>();
            
            this.Container.BindInterfacesTo<FireComponent>().AsSingle()
                .WithArguments(_fireSettings);
            
            this.Container.BindInterfacesTo<MoveComponent>().AsSingle()
                .WithArguments(_moveSettings);
            
            this.Container.BindInterfacesTo<HealthComponent>().AsSingle()
                .WithArguments(_healthSettings);
            
            this.Container.BindInterfacesTo<FollowComponent>().AsSingle()
                .WithArguments(_followSettings);
            
            this.Container.Bind<ShipView>().FromComponentOnRoot().AsSingle();

            this.Container.Bind<EnemyAI>().AsSingle();
        }
    }
}