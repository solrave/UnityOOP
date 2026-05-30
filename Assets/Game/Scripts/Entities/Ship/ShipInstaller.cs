using System;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    [Serializable]
    public class ShipInstaller : MonoInstaller
    {
        [Header("Settings")]
        [SerializeField] 
        private FireComponent.Settings _fireSettings;
        
        [SerializeField] 
        private MoveComponent.Settings  _moveSettings;
        
        [SerializeField]
        private HealthComponent.Settings _healthSettings;
        
        [SerializeField]
        private Rigidbody2D _rigidbody2D;
        
        [SerializeField] 
        private TeamComponent _teamComponent;

        public override void InstallBindings()
        {
            this.Container.Bind<Ship>()
                .AsSingle()
                .NonLazy();
            
            this.Container.Bind<TeamComponent>()
                .FromInstance(_teamComponent)
                .AsSingle();
            
            this.Container.BindInterfacesTo<FireComponent>()
                .AsSingle()
                .WithArguments(_fireSettings);
            
            this.Container.BindInterfacesTo<MoveComponent>()
                .AsSingle()
                .WithArguments(_moveSettings);
            
            this.Container.BindInterfacesTo<HealthComponent>()
                .AsSingle()
                .WithArguments(_healthSettings);
            
            Container.Bind<RigidbodyComponent>()
                .AsSingle()
                .WithArguments(_rigidbody2D);
        }
    }
}