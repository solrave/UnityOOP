using Game.Scripts.Components;
using Game.Scripts.Components.Core;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Context.GameObject.Ship
{
    public class Entity : GameObjectContext, IGameEntity
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

        public sealed class Pool : MemoryPool<Vector2,Vector2,Entity>
        {
            protected override void Reinitialize(Vector2 startPos, Vector2 firePos, Entity enemy)
            {
                enemy.Get<IMoveComponent>().SetPosition(startPos);
                enemy.Get<IFollowComponent>().SetDestination(firePos);
            }
            
            protected override void OnSpawned(Entity entity)
            {
                base.OnSpawned(entity);
                //entity.Initialize();
            }

            protected override void OnDespawned(Entity entity)
            {
                base.OnDespawned(entity);
            }
        }

    }
}