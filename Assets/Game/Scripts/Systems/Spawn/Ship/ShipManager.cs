using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class ShipManager : ITickable
    {
        [Serializable]
        public class Settings
        {
            [SerializeField] public float spawnCooldown;
        }
        
        public event Action OnKillCountIncrease;
        private float _lastSpawnedTime = 0f;
        private readonly Settings _settings;
        private readonly ShipSpawner _shipSpawner;
        private readonly PointService _pointService;
        private readonly List<Entity> _spawnedShips = new();
        
        protected ShipManager(Settings settings, ShipSpawner shipSpawner, PointService pointService)
        {
            _settings = settings;
            _shipSpawner = shipSpawner;
            _pointService = pointService;
        }

        public void Tick()
        {
            if (!TimeToSpawn()) return;
            var startPoint = _pointService.GetSpawnPoint().Position;
            var firePoint = _pointService.GetFirePoint().Position;
            var ship = _shipSpawner.Spawn();
            ship.Get<RigidbodyComponent>().Position = startPoint;
            ship.Get<EnemyAI>().SetFirePosition(firePoint);
            ship.Get<TeamComponent>().team = TeamType.Enemy;
            ship.Get<IHealthComponent>().OnHealthDepleted += this.Despawn;
            _spawnedShips.Add(ship);
        }
        
        private void Despawn(Entity ship)
        {
            OnKillCountIncrease?.Invoke();
            ship.Get<IHealthComponent>().OnHealthDepleted -= this.Despawn;
            _shipSpawner.Despawn(ship);
            _spawnedShips.Remove(ship);
        }

        private bool TimeToSpawn()
        {
            _lastSpawnedTime += Time.deltaTime;
            if (_lastSpawnedTime >= _settings.spawnCooldown)
            {
                _lastSpawnedTime = 0f;
                return true;
            }
            return false;
        }
        
        public void StopAllShips()
        {
            foreach (var ship in _spawnedShips)
            {
                ship.Get<EnemyAI>().SetTarget(null);
            }
        }
    } 
}

