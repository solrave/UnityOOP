using Game.Scripts.Components.Core;
using Zenject;

namespace Game.Scripts.Context.GameObject.Ship
{
    public class ShipComponents : IInitializable, MoveComponent.ICondition, FireComponent.ICondition
    {
        private readonly IHealthComponent _healthComponent;
        private readonly IMoveComponent _moveComponent;
        private readonly IFireComponent _fireComponent;
        private readonly IFollowComponent _followComponent;

        protected ShipComponents(IHealthComponent healthComponent, IMoveComponent moveComponent
            ,IFireComponent fireComponent, IFollowComponent followComponent)
        {
            _healthComponent = healthComponent;
            _moveComponent = moveComponent;
            _fireComponent = fireComponent;
            _followComponent = followComponent;
        }
        
        public virtual void Initialize()
        {
            _moveComponent.SetCondition(this);
            _fireComponent.SetCondition(this);
        }
        
        bool MoveComponent.ICondition.Evaluate() => _healthComponent.HasHealth;
        bool FireComponent.ICondition.Evaluate() => _healthComponent.HasHealth && !_moveComponent.IsMoving;
    }
}