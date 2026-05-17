using Game.Scripts.Components;

namespace Game.Scripts.Context.GameObject.Ship.Player
{
    public class PlayerShipInstaller : BaseComponentsInstaller
    {
        public override void InstallBindings()
        {
            base.InstallBindings();
            
            this.Container.BindInterfacesAndSelfTo<PlayerShipComponents>()
                .AsSingle()
                .NonLazy();
        }
    }
}