using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class EnemyManager : ITickable
    {
        [Serializable]
        public class Settings
        {
            [SerializeField] public float spawnCooldown;
        }
        
        public event Action OnKillCountIncrease;
        private float _lastSpawnedTime = 0f;
        private readonly Settings _settings;
        private readonly Entity.Pool _pool;
        private readonly PointService _pointService;
        private readonly List<Entity> _spawnedShips = new();
        private bool _spawnStop;
        
        protected EnemyManager(Settings settings,
            PointService pointService,
            Entity.Pool pool)
        {
            _settings = settings;
            _pointService = pointService;
            _pool = pool;
            _spawnStop = false;
        }

        public void Tick()
        {
            if (!TimeToSpawn() || _spawnStop) return;
            var startPoint = _pointService.GetSpawnPoint().Position;
            var firePoint = _pointService.GetFirePoint().Position;
            var ship = _pool.Spawn();
            ship.Get<RigidbodyComponent>().Position = startPoint;
            ship.Get<EnemyAI>().SetFirePosition(firePoint);
            ship.Get<TeamComponent>().Team = TeamType.Enemy;
            ship.Get<IHealthComponent>().OnHealthEmpty += OnHealthEmpty;
            ship.Get<IHealthComponent>().Initialize();
            _spawnedShips.Add(ship);
            return;

            void OnHealthEmpty()
            {
                ship.Get<IHealthComponent>().OnHealthEmpty -= OnHealthEmpty;
                this.Despawn(ship);
            }
        }
        
        private void Despawn(Entity ship)
        {
            OnKillCountIncrease?.Invoke();
            _pool.Despawn(ship);
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

        public void StopSpawn()
        {
            _spawnStop = true;
        }
    } 
}

