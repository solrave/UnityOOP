using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using Zenject;

namespace Game.Gameplay
{
    public class BulletManager : ILateTickable, IInitializable, IDisposable
    {
        private readonly Entity.Pool _pool;
        private PositionClamper _clamper;
        private List<Entity> _activeBullets = new();
        private Dictionary<Entity, Action> _bulletSubscription = new();

        public BulletManager
            ([Inject (Id = BindingID.BulletPositionClamper)] PositionClamper clamper, 
            Entity.Pool pool)
        {
            _clamper = clamper;
            _pool = pool;
        }
        
        public void Initialize()
        {
            _clamper.OnBulletOutOfRange += Despawn;
        }

        public void Dispose()
        {
            _clamper.OnBulletOutOfRange -= Despawn;
        }

        public void Spawn(TeamType team, Vector2 position, Vector2 direction)
        {
            var bullet = _pool.Spawn();
            bullet.Get<TeamComponent>().Team = team;
            bullet.Get<RigidbodyComponent>().Position = position;
            bullet.Get<RigidbodyComponent>().Rotation = Quaternion.LookRotation(direction, Vector3.up);
            bullet.Get<BulletView>().OnDestroy += OnDespawn;
            bullet.Get<BulletView>().EnableView();
            bullet.Get<IMoveComponent>().SetDirection(direction);
            bullet.gameObject.SetActive(true);
            _activeBullets.Add(bullet);
            _bulletSubscription.Add(bullet, OnDespawn);
            return;

            void OnDespawn()
            {
                bullet.Get<BulletView>().OnDestroy -= OnDespawn;
                this.Despawn(bullet);
            }
        }

        private void Despawn(Entity bullet)
        {
            _bulletSubscription.Remove(bullet, out var onDespawn);
            bullet.Get<BulletView>().OnDestroy -= onDespawn;
            _activeBullets.Remove(bullet);
            _pool.Despawn(bullet);
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
    }
}