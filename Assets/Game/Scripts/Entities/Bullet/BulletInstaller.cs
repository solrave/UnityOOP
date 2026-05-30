using Game.Gameplay;
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
        
        public override void InstallBindings()
        {
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