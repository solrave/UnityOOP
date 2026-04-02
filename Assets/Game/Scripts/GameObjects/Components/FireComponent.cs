using System;
using UnityEngine;

namespace Game.Components
{
    public class FireComponent : MonoBehaviour
    {
        public event Action<Transform> OnFire;
        
        [field: SerializeField]
        public TeamType Team { get;  private set;}
        
        [field: SerializeField]
        public Transform FirePoint { get; private set; }

        public bool HasTarget => _target != null;

        [SerializeField]
        private BulletSpawner _bulletSpawner;
        
        
        [SerializeField] 
        private float _fireCooldown = 0.25f;
        
        private float _fireTime;
        private Transform _target;

        public void SetTarget(Transform target) => _target = target;
        public void SetSpawner(BulletSpawner spawner) => _bulletSpawner = spawner;
        
        public void FireUp()
        {
            if (!TimeToShoot()) return;
            
            OnFire?.Invoke(FirePoint);
            var bullet = _bulletSpawner.Spawn(FirePoint,Team);
            bullet.SetDirection(FirePoint.up);
        }

        public void FireAt()
        {
            if (!TimeToShoot()) return;
            
            OnFire?.Invoke(FirePoint);
            var bullet = _bulletSpawner.Spawn(FirePoint,Team);
            var direction = (_target.position - transform.position).normalized;
            bullet.SetDirection(direction);
        }

        private bool TimeToShoot()
        {
            float time = Time.time;

            if (time - _fireTime < _fireCooldown)
                return false;
            
            _fireTime = time;
            return true;
        }
    }
}