using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Systems.Pool;
using Modules.Utils;
using UnityEngine;

namespace Game
{
    public class BulletSpawner : MonoBehaviour
    {
        [SerializeField]
        private Bullet _bulletPrefab;
        
        [SerializeField]
        private LevelBounds _levelBounds;
           
        private Pool<Bullet> _bulletPool;
        
        private readonly List<Bullet> _activeBullets = new();

        private void Awake()
        {
            _bulletPool = new(_bulletPrefab, 10);
        }

        private void Update()
        {
            CheckBulletsInBounds();
        }

        private void CheckBulletsInBounds()
        {
            if (_activeBullets.Count > 0)
            {
                for (int i = _activeBullets.Count - 1; i >= 0; i--)
                {
                    if (!_levelBounds.InBounds(_activeBullets[i].Position))
                    {
                        _bulletPool.Release(_activeBullets[i]);
                        _activeBullets.Remove(_activeBullets[i]);
                    }
                }
            }
        }

        public Bullet Spawn(Transform firePoint, TeamType team)
        {
            Bullet bullet = _bulletPool.Rent();
            bullet.SetTeam(team);
            bullet.SetPosition(firePoint.position);
            bullet.OnHit += _bulletPool.Release;
            _activeBullets.Add(bullet);
            Debug.Log($"Bullets { _activeBullets.Count}");
            return bullet;
        }
    }
}