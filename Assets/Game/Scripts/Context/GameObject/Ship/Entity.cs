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
            private readonly TickableManager _tickableManager;

            public Pool(TickableManager tickableManager)
            {
                _tickableManager = tickableManager;
            }

            protected override void Reinitialize(Vector2 startPos, Vector2 firePos, Entity enemy)
            {
                enemy.Get<IMoveComponent>().SetPosition(startPos);
                enemy.Get<IFollowComponent>().SetDestination(firePos);
            }
            
            protected override void OnSpawned(Entity entity)
            {
                base.OnSpawned(entity);
                entity.Initialize();
                _tickableManager.AddFixed(entity.Get<MoveComponent>());
                
                //entity.OnDispose += this.Despawn;
            }

            protected override void OnDespawned(Entity entity)
            {
                _tickableManager.RemoveFixed(entity.Get<MoveComponent>());
                base.OnDespawned(entity);
                
                //entity.Get<ShipComponents>().OnDispose -= this.Despawn;
                //entity.Dispose();
            }
        }

    }
}