using System;
using System.Collections.Generic;
using System.Linq;
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
        
        private readonly List<Bullet> _pool = new();
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
            if (_activeBullets.Count > 0)
            {
                for (int i = _activeBullets.Count - 1; i >= 0; i--)
                {
                    if (!_levelBounds.InBounds(_activeBullets[i].transform.position))
                    {
                        ReleaseBullet(_activeBullets[i]);
                    }
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

        public Bullet Spawn(Transform firePoint, TeamType team)
        {
            Bullet bullet = _pool.FirstOrDefault(b => b.Team == team);
            
            if (bullet != null)
            {
                bullet.transform.position = firePoint.position;
                bullet.gameObject.SetActive(true);
                _pool.Remove(bullet);
            }
            else
                bullet = Instantiate(_bulletLibrary[team],firePoint.position,Quaternion.identity);
            
            bullet.transform.position = firePoint.position;
            bullet.OnHit += this.ReleaseBullet;
            _activeBullets.Add(bullet);
            return bullet;
        }

        private void ReleaseBullet(Bullet  bullet)
        {
            bullet.gameObject.SetActive(false);
            _pool.Add(bullet);
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