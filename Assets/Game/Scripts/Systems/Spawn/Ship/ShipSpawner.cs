using System;
using System.Collections.Generic;
using Zenject;

namespace Game.Gameplay
{
    public class ShipSpawner
    {
        private readonly Entity.Pool _pool;

        public ShipSpawner([Inject(Id = ID.ShipPool)]Entity.Pool pool)
        {
            _pool = pool;
        }

        public Entity Spawn()
        {
            return _pool.Spawn();
        }

        public void Despawn(Entity ship)
        {
            _pool.Despawn(ship);
        }
    }
}