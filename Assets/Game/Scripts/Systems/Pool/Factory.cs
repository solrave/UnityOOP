using UnityEngine;

namespace Game.Scripts.Systems.Pool
{
    public class Factory<T> where T : MonoBehaviour
    {
        private readonly T _prefab;

        public Factory(T prefab)
        {
            _prefab = prefab;
        }

        public T Create()
        {
            T obj = GameObject.Instantiate(_prefab);
            obj.gameObject.SetActive(false);
            return obj;
        }

    }
}