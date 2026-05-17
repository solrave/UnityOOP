using Game.Scripts.Components;
using Zenject;

namespace Game.Scripts.Context.GameObject.Ship
{
    public class GameEntity : GameObjectContext, IGameEntity
    {
        public string Name { get; set; }

        public T Get<T>() where T : class
        {
            return this.Container.Resolve<T>();
        }

        public bool TryGet<T>(out T result) where T : class
        {
            result = this.Container.TryResolve<T>();
            return result != null;
        }
    }
}