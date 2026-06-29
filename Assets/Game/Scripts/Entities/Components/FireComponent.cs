using System;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public interface IFireComponent
    {
        public float Cooldown { get; }
        public Vector2 GunPoint {get;}
        public event Action OnFire;
        public void FireUp();
        public void FireAt(Vector2 direction);
        public void SetCondition(FireComponent.ICondition condition);
        
    }
    public class FireComponent : IFireComponent, ITickable
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

        public float Cooldown => _settings.FireCooldown;
        public Vector2 GunPoint => _settings.GunPoint.position;
        public event Action OnFire;
        
        private readonly BulletManager _bulletManager;
        private readonly Settings _settings;
        private ICondition _condition;
        private float _time;
        private bool _canShoot;
        private TeamComponent _teamComponent;
        
        public FireComponent(
            BulletManager bulletManager,
            Settings settings,
            TeamComponent teamComponent)
        {
            _bulletManager = bulletManager;
            _settings = settings;
            _teamComponent = teamComponent;
        }
        
        public void SetCondition(ICondition condition) => _condition = condition;
        
        public void FireUp()
        {
            if (!_condition.Evaluate()) return;
            
            _bulletManager.Spawn(_teamComponent.Team,_settings.GunPoint.position,_settings.GunPoint.up);
            _canShoot = false;
            _time = 0f;
            OnFire?.Invoke();
        }

        public void FireAt(Vector2 direction)
        {
            if (!_condition.Evaluate() || !_canShoot) return;
            
            _bulletManager.Spawn(_teamComponent.Team,_settings.GunPoint.position,direction);
            ResetCooldown();
            OnFire?.Invoke();
        }

        private void ResetCooldown()
        {
            _canShoot = false;
            _time = 0f;
        }

        private void TimeToShoot()
        {
            if(_canShoot) return;
            
            _time += Time.deltaTime;
            
            if (_time >= _settings.FireCooldown)
            {
                _time = 0f;
                _canShoot = true;
            }
        }

        public void Tick()
        {
            TimeToShoot();
        }
    }
}