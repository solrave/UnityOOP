using Game.Scripts.Components;
using Game.Scripts.Components.Core;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Context.GameObject.Ship
{
    public class Entity : GameObjectContext, IGameEntity, IPoolable
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

        public sealed class Pool : MonoMemoryPool<Vector2,Vector2,Entity>
        {
            protected override void Reinitialize(Vector2 startPos, Vector2 firePos, Entity enemy)
            {
                enemy.Get<IMoveComponent>().SetPosition(startPos);//TransfformComponent
                enemy.Get<IFollowComponent>().SetDestination(firePos);
            }
            
            protected override void OnSpawned(Entity entity)
            {
                base.OnSpawned(entity);
                
            }
            
            protected override void OnDespawned(Entity entity)
            {
                base.OnDespawned(entity);
            }
        }

        public void OnDespawned()
        {
            //OnDisable
        }

        public void OnSpawned()
        {
           //OnEnable
        }
    }
}