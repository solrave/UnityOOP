using System;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class FireComponent : IFireComponent
    {
        [Serializable]
        public sealed class Settings
        {
            [field: SerializeField] 
            public float FireCooldown { get; private set; }

            [field: SerializeField] 
            public Transform GunPoint { get; private set; }
        }
        
        public interface ICondition
        {
            bool Evaluate();
        }

        public TeamType Team => _teamComponent.team;
        public event Action OnFire;
        public bool ReadyToShoot => TimeToShoot();
        
        private readonly IMemoryPool<Bullet.BulletSettings, Bullet> _bulletSpawner;
        private readonly Settings _settings;
        private ICondition _condition;
        private float _fireTime;
        private TeamComponent _teamComponent;
        
        public FireComponent(IMemoryPool<Bullet.BulletSettings, Bullet> bulletSpawner,
                             Settings settings, TeamComponent teamComponent)
        {
            _bulletSpawner = bulletSpawner;
            _settings = settings;
            _teamComponent = teamComponent;
        }
        
        public void SetCondition(ICondition condition) => _condition = condition;
        
        public void FireUp()
        {
            if (!TimeToShoot()) return;
            OnFire?.Invoke();
            _bulletSpawner.Spawn(new Bullet.BulletSettings
            {
                team = Team,
                position = _settings.GunPoint.position,
                direction =_settings.GunPoint.up
            });
        }

        public void FireAt(Vector2 direction)
        {
            if (!TimeToShoot()) return;
            
            OnFire?.Invoke();
            _bulletSpawner.Spawn(new Bullet.BulletSettings
            {
                team = Team,
                position = _settings.GunPoint.position,
                direction = direction
            });
        }

        private bool TimeToShoot()
        {
            float time = Time.time;

            if (time - _fireTime < _settings.FireCooldown)
                return false;

            _fireTime = time;
            return true;
        }
    }

    public interface IFireComponent
    {
        public TeamType Team { get; }
        public event Action OnFire;
        public bool ReadyToShoot { get; }
        public void FireUp();
        public void FireAt(Vector2 direction);
        public void SetCondition(FireComponent.ICondition condition);
    }
}