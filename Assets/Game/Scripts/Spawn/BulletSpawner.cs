using System;
using System.Collections.Generic;
using Modules.Utils;
using UnityEngine;

namespace Game
{
    public class BulletSpawner : MonoBehaviour
    {
        [SerializeField]
        private List<Bullet> _bulletPrefabs;
        
        [SerializeField]
        private LevelBounds _levelBounds;
        
        private readonly Stack<Bullet> _pool = new();
        private readonly Dictionary<TeamType, Bullet> _bulletLibrary = new();
        private readonly List<Bullet> _activeBullets = new();
        
        private void Awake()
        {
            PopulateLibraryFromList();
        }

        private void Update()
        {
            CheckBulletsInBounds();
        }

        private void CheckBulletsInBounds()
        {
            foreach (Bullet activeBullet in _activeBullets)
            {
                if (!_levelBounds.InBounds(activeBullet.transform.position))
                {
                    ReleaseBullet(activeBullet);
                }
            }
        }

        private void PopulateLibraryFromList()
        {
            foreach (Bullet bullet in _bulletPrefabs)
            {
                _bulletLibrary.Add(bullet.Team, bullet);
            }
        }

        public void Spawn(Transform firePoint, TeamType team)
        {
            if (_pool.TryPop(out Bullet bullet))
                bullet.gameObject.SetActive(true);
            else
                bullet = Instantiate(_bulletLibrary[team], firePoint);
            
            bullet.transform.position = firePoint.position;
            bullet.SetDirection(firePoint.up);
            bullet.gameObject.layer = bullet.Team switch
            {
                TeamType.None => LayerMask.NameToLayer("Default"),
                TeamType.Player => LayerMask.NameToLayer("PlayerBullet"),
                TeamType.Enemy => LayerMask.NameToLayer("EnemyBullet"),
                _ => throw new ArgumentOutOfRangeException(nameof(bullet.Team), bullet.Team, null)
            };
            
            bullet.OnHit += this.ReleaseBullet;
            _activeBullets.Add(bullet);
        }

        private void ReleaseBullet(Bullet  bullet)
        {
            bullet.gameObject.SetActive(false);
            _pool.Push(bullet);
            bullet.OnHit -= this.ReleaseBullet;
            _activeBullets.Remove(bullet);
        }

        // private void MoveBullets()
        // {
        //     for (int i = _bullets.Count - 1; i >= 0; i--)
        //     {
        //         Bullet bullet = _bullets[i];
        //         Vector3 moveStep = bullet.Direction * (bullet.Speed * Time.fixedDeltaTime);
        //         bullet.transform.position += moveStep;
        //
        //         if (!_levelBounds.InBounds(bullet.transform.position))
        //         {
        //             bullet.OnHit -= this.OnBulletHit;
        //             bullet.gameObject.SetActive(false);
        //             _pool.Push(bullet);
        //         }
        //     }
        // }
    }
}