using Zenject;

namespace Game.Gameplay
{
    public class Ship : IInitializable, MoveComponent.ICondition, FireComponent.ICondition
    {
        private TeamComponent _teamComponent;
        private readonly IHealthComponent _healthComponent;
        private readonly IMoveComponent _moveComponent;
        private readonly IFireComponent _fireComponent;

        protected Ship(IHealthComponent healthComponent,
                       IMoveComponent moveComponent,
                       IFireComponent fireComponent,
                       TeamComponent teamComponent)
        {
            _healthComponent = healthComponent;
            _moveComponent = moveComponent;
            _fireComponent = fireComponent;
            _teamComponent = teamComponent;
        }
        
        public virtual void Initialize()
        {
            _moveComponent.SetCondition(this);
            _fireComponent.SetCondition(this);
        }
        
        bool MoveComponent.ICondition.Evaluate() => _healthComponent.HasHealth;
        bool FireComponent.ICondition.Evaluate() => _healthComponent.HasHealth;
    }
}