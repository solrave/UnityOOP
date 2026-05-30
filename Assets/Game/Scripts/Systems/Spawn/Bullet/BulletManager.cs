using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class BulletManager : ILateTickable
    {
        private BulletSpawner _spawner;
        private PositionClamper _clamper;
        private List<Entity> _activeBullets = new();

        public BulletManager(BulletSpawner spawner,[Inject (Id = ID.BulletPositionClamper)] PositionClamper clamper)
        {
            _spawner = spawner;
            _clamper = clamper;
        }

        public void Spawn(TeamType team, Vector2 position, Vector2 direction)
        {
            var bullet = _spawner.Spawn(team, position, direction);
            _activeBullets.Add(bullet);
        }
        
        public void LateTick()
        {
            ClampBulletsInBounds();
        }

        private void ClampBulletsInBounds()
        {
            for (int i = 0; i < _activeBullets.Count; i++)
            {
                if (_clamper.ClampInLevelBounds(_activeBullets[i]))
                {
                    _activeBullets.Remove(_activeBullets[i]);
                }
            }
        }
    }
}