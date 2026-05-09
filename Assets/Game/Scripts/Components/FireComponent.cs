using System;
using UnityEngine;

namespace Game.Components
{
    [Serializable]
    public class FireComponent
    {
        public event Action OnFire;

        [SerializeField]
        private Transform _firePoint;
        
        [SerializeField]
        private BulletSpawner _bulletSpawner;
        
        [SerializeField] 
        private float _fireCooldown = 0.25f;
        
        private float _fireTime;
        
        public void SetSpawner(BulletSpawner spawner) => _bulletSpawner = spawner;
        
        public void FireUp(TeamType type)
        {
            if (!TimeToShoot()) return;
            OnFire?.Invoke();
            var bullet = _bulletSpawner.Spawn(_firePoint.position, type);
            bullet.gameObject.SetActive(true);
            bullet.SetDirection(_firePoint.up);
        }

        public void FireAt(TeamType type, Vector2 direction)
        {
            if (!TimeToShoot()) return;
            
            OnFire?.Invoke();
            var bullet = _bulletSpawner.Spawn(_firePoint.position, type);
            bullet.gameObject.SetActive(true);
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