using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField]
        private EnemyAI.Settings _aiSettings;
        
        public override void InstallBindings()
        {
            this.Container
                .BindInterfacesAndSelfTo<EnemyAI>()
                .AsSingle()
                .WithArguments(_aiSettings);
            
            this.Container
                .Bind<EnemyAI.Settings>()
                .AsSingle()
                .WithArguments(_aiSettings);
        }
    }
}