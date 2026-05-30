using Game.Gameplay;
using Modules.Utils;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Context.GameObject.Bullet
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
        private LevelBounds _bulletBounds;
        
        public override void InstallBindings()
        {
            this.Container
                .BindInterfacesTo<PositionClamper>()
                .AsSingle()
                .NonLazy();
            
            this.Container.Bind<TeamComponent>()
                .FromInstance(_teamComponent)
                .AsSingle();
            
            this.Container.Bind<DamageComponent>()
                .FromInstance(_damageComponent)
                .AsSingle();
            
            Container.Bind<RigidbodyComponent>().AsSingle()
                .WithArguments(_rigidbody2D);

            this.Container.Bind<CollisionListener>().AsSingle();
            this.Container.Bind<TeamComponent>().AsSingle();
        }
    }
}