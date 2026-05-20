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
            this.Container.BindInterfacesTo<FireComponent>().AsSingle()
                .WithArguments(_fireSettings);
            
            this.Container.BindInterfacesTo<MoveComponent>().AsSingle()
                .WithArguments(_moveSettings);
            
            this.Container.BindInterfacesTo<HealthComponent>().AsSingle()
                .WithArguments(_healthSettings);
            
            this.Container.BindInterfacesTo<FollowComponent>().AsSingle()
                .WithArguments(_followSettings);
            
            this.Container.Bind<ShipView>().FromComponentInHierarchy().AsSingle();

            this.Container.Bind<EnemyAI>().AsSingle();
            
            this.Container.BindInterfacesAndSelfTo<ShipComponents>()
                .AsSingle()
                .NonLazy();
            
        }
    }
}