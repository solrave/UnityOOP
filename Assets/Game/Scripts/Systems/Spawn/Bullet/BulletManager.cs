using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class BulletManager : ILateTickable, IInitializable, IDisposable
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
            var bullet = _spawner.Spawn();
            bullet.Get<TeamComponent>().team = team;
            bullet.Get<RigidbodyComponent>().Position = position;
            bullet.Get<RigidbodyComponent>().Rotation = Quaternion.LookRotation(direction, Vector3.up);
            bullet.Get<IMoveComponent>().SetDirection(direction);
            bullet.Get<Bullet>().OnHit += this.Despawn;
            bullet.Get<Bullet>().Initialize();
            bullet.gameObject.GetComponent<BulletView>().Enable();
            bullet.gameObject.SetActive(true);
            _activeBullets.Add(bullet);
        }

        private void Despawn(Entity bullet)
        {
            _activeBullets.Remove(bullet);
            bullet.Get<Bullet>().OnHit -= this.Despawn;
            _spawner.Despawn(bullet);
           bullet.gameObject.SetActive(false);
        }

        public void LateTick()
        {
            ClampBulletsInBounds();
        }

        private void ClampBulletsInBounds()
        {
            if (_activeBullets.Count > 0)
            {
                for(int i = 0; i < _activeBullets.Count; i++)
                {
                    _clamper.ClampInLevelBounds(_activeBullets[i]);
                }
            }
        }

        public void Initialize()
        {
            _clamper.OnDestroyBullet += Despawn;
        }

        public void Dispose()
        {
            _clamper.OnDestroyBullet -= Despawn;

        }
    }
}