using Game.Gameplay;
using Modules.Utils;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class BulletInstaller : MonoInstaller
    {
        [SerializeField] 
        private TeamComponent _teamComponent;
        
        [SerializeField]
        private DamageComponent _damageComponent;
        
        [SerializeField]
        private Rigidbody2D _rigidbody2D;
        
        [SerializeField] 
        private MoveComponent.Settings  _moveSettings;
        
        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<Entity>()
                .FromComponentInHierarchy()
                .AsSingle();
            
            this.Container.BindInterfacesAndSelfTo<Bullet>()
                .AsSingle()
                .NonLazy();

            this.Container.BindInterfacesAndSelfTo<BulletView>()
                .FromComponentInHierarchy()
                .AsSingle();
            
            this.Container.BindInterfacesAndSelfTo<MoveComponent>()
                .AsSingle()
                .WithArguments(_moveSettings);
            
            this.Container.BindInterfacesAndSelfTo<TeamComponent>()
                .FromInstance(_teamComponent)
                .AsCached()
                .Lazy();
            
            this.Container.BindInterfacesAndSelfTo<DamageComponent>()
                .FromInstance(_damageComponent)
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<RigidbodyComponent>()
                .AsSingle()
                .WithArguments(_rigidbody2D);

            this.Container.BindInterfacesAndSelfTo<CollisionObservable>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
    }
}