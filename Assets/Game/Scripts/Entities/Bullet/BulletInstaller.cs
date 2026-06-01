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

        [SerializeField] 
        private CollisionListener _collisionListener;
        
        
        public override void InstallBindings()
        {
            this.Container.Bind<Bullet>()
                .AsSingle()
                .NonLazy();
            
            this.Container.BindInterfacesTo<MoveComponent>()
                .AsSingle()
                .WithArguments(_moveSettings);
            
            this.Container.Bind<TeamComponent>()
                .FromInstance(_teamComponent)
                .AsCached();
            
            this.Container.Bind<DamageComponent>()
                .FromInstance(_damageComponent)
                .AsSingle();
            
            Container.Bind<RigidbodyComponent>().AsSingle()
                .WithArguments(_rigidbody2D);

            this.Container.Bind<CollisionListener>()
                .FromInstance(_collisionListener)
                .AsSingle();
            
            // this.Container.Bind<TeamComponent>()
            //     .FromInstance(_teamComponent)
            //     .AsSingle();
        }
    }
}