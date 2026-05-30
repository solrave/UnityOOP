using UnityEngine;

namespace Game.Gameplay
{
    public class BulletSpawner
    {
        private readonly Entity.Pool _pool;
        private BulletManager _manager;

        public BulletSpawner(Entity.Pool pool, BulletManager manager)
        {
            _pool = pool;
            _manager = manager;
        }

        public void Spawn(TeamType team, Vector2 position, Vector2 direction)
        {
            var bullet = _pool.Spawn();
            _manager.Add(bullet);
            bullet.Get<TeamComponent>().team = team;
            bullet.Get<RigidbodyComponent>().Position = position;
            bullet.Get<IMoveComponent>().SetDirection(direction);
        }
    }
}