namespace DefaultNamespace
{
    public class ShipSpawner
    {
        public event Action OnKillCountIncrease;
        private readonly PointService _pointService;
        private readonly Entity.Pool _pool;
        private readonly List<Entity> _spawnedShips = new();

        public ShipSpawner(Entity.Pool pool, PointService pointService)
        {
            _pool = pool;
            _pointService = pointService;
        }

        public void Spawn()
        {
            var startPoint = _pointService.GetSpawnPoint().Position;
            var firePoint = _pointService.GetFirePoint().Position;
            var ship = _pool.Spawn();
            ship.Get<RigidbodyComponent>().SetPosition(startPoint);
            ship.Get<EnemyAI>().SetFirePosition(startPoint);
            ship.Get<IHealthComponent>().OnHealthDepleted += this.Despawn;
            _spawnedShips.Add(ship);
        }

        private void Despawn(Entity ship)
        {
            OnKillCountIncrease?.Invoke();
            _spawnedShips.Remove(ship);
            _pool.Despawn(ship);
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