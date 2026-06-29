using System;
using Modules.Utils;

namespace Game.Gameplay
{
    public class PositionClamper
    {
        private readonly LevelBounds _levelBounds;
        
        public PositionClamper(LevelBounds levelBounds)
        { 
            _levelBounds = levelBounds;
        }

        public void ClampInLevelBounds(Entity entity)
        {
            if (!_levelBounds.InBounds(entity.Get<RigidbodyComponent>().Position))
            {
                var newPosition = _levelBounds.ClampInBounds(entity.Get<RigidbodyComponent>().Position);
                
                if (entity.TryGet<Bullet>(out var bullet))
                {
                    bullet.ApplyHit();
                }
                
                if (entity.TryGet<Ship>(out var ship))
                {
                    entity.Get<RigidbodyComponent>().Position = newPosition;
                }
            }
        }
    }
}