using Game.Scripts.Components;
using Zenject;

namespace Game.Scripts.Context.GameObject.Ship
{
   
    public class Entity : GameObjectContext, IEntity
    {
        public sealed class Pool : MonoMemoryPool<Entity>
        {
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