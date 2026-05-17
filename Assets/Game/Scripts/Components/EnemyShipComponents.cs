using Game.Scripts.Components.Core;
using Game.Scripts.Context.GameObject.Ship;

namespace Game.Scripts.Components
{
    public class EnemyShipComponents : ShipComponents
    {
        private IFollowComponent _followComponent;
        public EnemyShipComponents(IHealthComponent healthComponent,
            IMoveComponent moveComponent, IFireComponent fireComponent, IFollowComponent followComponent)
            : base(healthComponent, moveComponent, fireComponent)
        {
            _followComponent = followComponent;
        }
    }
}