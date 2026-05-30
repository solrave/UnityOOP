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
            bullet.Get<Bullet>().OnDispose += this.Despawn;
            bullet.Get<RigidbodyComponent>().Body.gameObject.SetActive(true);
            _activeBullets.Add(bullet);
        }

        private void Despawn(Entity bullet)
        {
            _activeBullets.Remove(bullet);
            bullet.Get<Bullet>().OnDispose -= this.Despawn;
            bullet.Get<RigidbodyComponent>().Body.gameObject.SetActive(false);
        }

        public void LateTick()
        {
            ClampBulletsInBounds();
        }

        private void ClampBulletsInBounds()
        {
            if (_activeBullets.Count > 0)
            {
                foreach (var bullet in _activeBullets)
                {
                    _clamper.ClampInLevelBounds(bullet);
                }
            }
        }
    }
}