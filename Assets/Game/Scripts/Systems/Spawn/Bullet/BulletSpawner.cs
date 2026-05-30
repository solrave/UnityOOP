using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class BulletSpawner
    {
        private readonly Entity.Pool _pool;

        public BulletSpawner([Inject(Id = ID.BulletPool)]Entity.Pool pool)
        {
            _pool = pool;
        }

        public Entity Spawn(TeamType team, Vector2 position, Vector2 direction)
        {
            var bullet = _pool.Spawn();
            bullet.Get<TeamComponent>().team = team;
            bullet.Get<RigidbodyComponent>().Position = position;
            bullet.Get<IMoveComponent>().SetDirection(direction);
            return bullet;
        }
    }
}