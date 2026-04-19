using UnityEngine;
using System.Collections.Generic;

namespace Game.Scripts.Systems.Pool
{
    public class Pool<T> where T : MonoBehaviour
    {
        private readonly Queue<T> _pool;
        private readonly T _prefab;

        public Pool(T prefab, int prewarmObjects)
        {
            _prefab = prefab;
            _pool = new Queue<T>(prewarmObjects);
            
            for (int i = 0; i < _pool.Count; i++)
            {
                T obj = GameObject.Instantiate(_prefab);
                obj.gameObject.SetActive(false);
                _pool.Enqueue(obj);
            }
        }

        public T Rent()
        {
            if (_pool.TryDequeue(out T obj))
            {
                obj.gameObject.SetActive(false);
                return obj;
            }

            return Create();
        }

        private T Create()
        {
            T obj = GameObject.Instantiate(_prefab);
            obj.gameObject.SetActive(false);
            return obj;
        }

        public void Release(T obj)
        {
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
        }
    }
}