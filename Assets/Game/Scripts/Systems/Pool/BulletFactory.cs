using UnityEngine;

namespace Game.Scripts.Systems.Pool
{
    public class BulletFactory : Factory<Bullet>
    {
        public BulletFactory(Bullet prefab) : base(prefab)
        {
        }
    }
}