namespace Game.Scripts.Systems.Pool
{
    public class BulletPool : Pool<Bullet>
    {
        public BulletPool(Bullet prefab, int prewarmObjects) : base(prefab, prewarmObjects)
        {
        }
    }
}