using Zenject;

namespace Game.Gameplay
{
   
    public class Entity : GameObjectContext, IEntity
    {
        public sealed class Pool : MonoMemoryPool<Entity>
        {
            protected override void Reinitialize(Entity item)
            {
                //item.Get<Bullet>().ResetFlag();
            }
        }
        
        public string Name
        {
            get => name;
            set => name = value;
        }

        public T Get<T>() where T : class
        {
            var result = this.Container.Resolve<T>();
            return result;
        }

        public bool TryGet<T>(out T result) where T : class
        {
            result = this.Container.TryResolve<T>();
            return result != null;
        }
    }
}