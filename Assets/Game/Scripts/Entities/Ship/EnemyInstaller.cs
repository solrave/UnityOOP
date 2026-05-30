using Game.Scripts.Entities.Components;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField]
        private AISettings _followSettings;

        [SerializeField]
        private Rigidbody2D _rigidbody2D;
        
        
        public override void InstallBindings()
        {
            this.Container
                .Bind<EnemyAI>()
                .AsSingle()
                .WithArguments(_followSettings);
            
            this.Container
                .BindInterfacesTo<AISettings>()
                .AsSingle()
                .WithArguments(_followSettings);
            
            Container.Bind<RigidbodyComponent>().AsSingle()
                .WithArguments(_rigidbody2D);

            this.Container.Bind<CollisionListener>().AsSingle();
            this.Container.Bind<TeamComponent>().AsSingle();
        }
    }
}