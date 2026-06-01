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

        public Entity Spawn()
        {
            var bullet = _pool.Spawn();
            return bullet;
        }

        public void Despawn(Entity bullet)
        {
            _pool.Despawn(bullet);
        }
    }
}