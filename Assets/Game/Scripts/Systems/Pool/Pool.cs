using UnityEngine;
using System.Collections.Generic;

namespace Game.Scripts.Systems.Pool
{
    public class Pool<T> where T : MonoBehaviour
    {
        private Queue<T> _pool;
        private readonly Factory<T> _factory;

        public Pool(T prefab, int prewarmObjects)
        { 
            _factory = new Factory<T>(prefab);
            PrewarmObjects(prewarmObjects);
        }
        
        public T Rent()
        {
            if (_pool.TryDequeue(out T obj))
            {
                obj.gameObject.SetActive(false);
                return obj;
            }

            return _factory.Create();
        }

        public void Release(T obj)
        {
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
        }
        
        private void PrewarmObjects(int prewarmObjects)
        {
            _pool = new Queue<T>(prewarmObjects);
            for (int i = 0; i < _pool.Count; i++)
            {
                T obj = _factory.Create();
                obj.gameObject.SetActive(false);
                _pool.Enqueue(obj);
            }
        }
    }

    public interface ISpawnableShip
    {
        void Setup(Vector2 spawnPoint, Vector2 firePoint, Ship target, BulletSpawner bulletSpawner);
    }
    
    public interface ISpawnableBullet
    {
        void Setup(TeamType team, Vector2 position);
    }
    
}