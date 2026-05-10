using System;
using UnityEngine;
using Zenject;

namespace Game.Components
{
    [Serializable]
    public class FireComponent : IFireComponent
    {
        public event Action OnFire;
        
        private Transform _firePoint;
        private IMemoryPool<Bullet.BulletCreateArgs, Bullet> _bulletSpawner;
        
        private float _fireCooldown;
        private float _fireTime;
        
        [Inject]
        public FireComponent(IMemoryPool<Bullet.BulletCreateArgs, Bullet> bulletSpawner
            ,Transform firePoint, float fireCooldown)
        {
            _bulletSpawner = bulletSpawner;
            _firePoint = firePoint;
            _fireCooldown = fireCooldown;
        }
        
        public void FireUp(TeamType team)
        {
            if (!TimeToShoot()) return;
            OnFire?.Invoke();
            var bullet = _bulletSpawner.Spawn(new Bullet.BulletCreateArgs
            {
                team = team,
                position = _firePoint.position,
                direction = _firePoint.up
            });
        }

        public void FireAt(TeamType team, Vector2 direction)
        {
            if (!TimeToShoot()) return;
            
            OnFire?.Invoke();
            var bullet = _bulletSpawner.Spawn(new Bullet.BulletCreateArgs
            {
                team = team,
                position = _firePoint.position,
                direction = direction
            });
        }

        private bool TimeToShoot()
        {
            float time = Time.time;

            if (time - _fireTime < _fireCooldown)
                return false;
            
            _fireTime = time;
            return true;
        }
    }

    public interface IFireComponent
    {
        public event Action OnFire;
        public void FireUp(TeamType team);
        public void FireAt(TeamType type, Vector2 direction);
    }
}