using System.Collections.Generic;
using Zenject;

namespace Game.Gameplay
{
    public class BulletManager : ILateTickable
    {
        private BulletSpawner _spawner;
        private PositionClamper _clamper;
        private List<Entity> _activeBullets = new();

        public BulletManager(BulletSpawner spawner, PositionClamper clamper)
        {
            _spawner = spawner;
            _clamper = clamper;
        }

        public void Add(Entity bullet)
        {
            _activeBullets.Add(bullet);
        }

        public void LateTick()
        {
            foreach (var bullet in _activeBullets)
            {
                _clamper.ClampInLevelBounds(bullet);
            }
        }
    }
}