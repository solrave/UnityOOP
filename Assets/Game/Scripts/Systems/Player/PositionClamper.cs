using Game.Scripts.Context.GameObject.Ship;
using Modules.Utils;
using Zenject;

namespace Game.Gameplay
{
    public class PositionClamper : ILateTickable
    {
        private readonly Entity _entity;
        private readonly LevelBounds _levelBounds;
        
        public PositionClamper(Entity entity, LevelBounds levelBounds)
        {
            _entity = entity;
            _levelBounds = levelBounds;
        }

        public void LateTick() => ClampInLevelBounds();
        
        private void ClampInLevelBounds()
        {
            if (!_levelBounds.InBounds(_entity.Get<RigidbodyComponent>().Position))
            {
                var newPosition = _levelBounds.ClampInBounds(_entity.Get<IMoveComponent>().Position);
                
                if (_entity.TryGet<Bullet>(out var bullet))
                {
                    bullet.IsExpired();
                }
                
                _entity.Get<RigidbodyComponent>().Position = newPosition;
            }
        }
    }
}