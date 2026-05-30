using System;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public interface IFireComponent
    {
        public TeamType Team { get; }
        public Vector2 GunPoint {get;}
        public event Action OnFire;
        public bool ReadyToShoot { get; }
        public void FireUp();
        public void FireAt(Vector2 direction);
        public void SetCondition(FireComponent.ICondition condition);
        
    }
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
        public Vector2 GunPoint => _settings.GunPoint.position;
        public event Action OnFire;
        public bool ReadyToShoot => TimeToShoot();
        
        private readonly BulletSpawner _bulletSpawner;
        private readonly Settings _settings;
        private ICondition _condition;
        private float _fireTime;
        private TeamComponent _teamComponent;
        
        public FireComponent(BulletSpawner bulletSpawner, Settings settings, TeamComponent teamComponent)
        {
            _bulletSpawner = bulletSpawner;
            _settings = settings;
            _teamComponent = teamComponent;
        }
        
        public void SetCondition(ICondition condition) => _condition = condition;
        
        public void FireUp()
        {
            if (!TimeToShoot() && !_condition.Evaluate()) return;
            
            OnFire?.Invoke();
            _bulletSpawner.Spawn(Team,_settings.GunPoint.position,_settings.GunPoint.up);
        }

        public void FireAt(Vector2 direction)
        {
            if (!TimeToShoot() && !_condition.Evaluate()) return;
            
            OnFire?.Invoke();
            _bulletSpawner.Spawn(Team,_settings.GunPoint.position,direction);
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
}