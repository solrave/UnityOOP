using System;
using Game.Scripts.Components;
using Game.Scripts.Components.Core;
using Game.Scripts.GameObjects.Ship;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Context.GameObject.Ship
{
    [Serializable]
    public class BaseComponentsInstaller : MonoInstaller
    {
        [Header("Settings")]
        [SerializeField] 
        private FireComponent.Settings _fireSettings;
        
        [SerializeField] 
        private MoveComponent.Settings  _moveSettings;
        
        [SerializeField]
        private HealthComponent.Settings _healthSettings;

        public override void InstallBindings()
        {
            this.Container.BindInterfacesTo<FireComponent>().AsSingle()
                .WithArguments(_fireSettings);
            
            this.Container.BindInterfacesTo<MoveComponent>().AsSingle()
                .WithArguments(_moveSettings);
            
            this.Container.BindInterfacesTo<HealthComponent>().AsSingle()
                .WithArguments(_healthSettings);
            
            Container.Bind<ShipView>().FromComponentInHierarchy().AsSingle();
        }
    }
}