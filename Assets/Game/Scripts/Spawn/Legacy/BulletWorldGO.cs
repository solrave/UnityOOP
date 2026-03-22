// using System;
// using System.Collections.Generic;
// using Modules.Utils;
// using UnityEngine;
//
// namespace Game
// {
//     // +
//     public sealed class BulletWorldGO : MonoBehaviour
//     {
//         [SerializeField]
//         private Bullet _prefab;
//
//
//         [SerializeField]
//         private BulletViewConfig _configView;
//
//         [SerializeField]
//         private TransformBounds _levelBounds;
//
//         [SerializeField]
//         private Transform _container;
//         private readonly Stack<Bullet> _pool = new();
//         private readonly List<Bullet> _bullets = new();
//
//         private void Awake()
//         {
//             for (var i = 0; i < 10; i++)
//             {
//                 Bullet bullet = Instantiate(_prefab, _container);
//                 bullet.gameObject.SetActive(false);
//                 _pool.Push(bullet);
//             }
//         }
//
//         private void FixedUpdate()
//         {
//             for (int i = _bullets.Count - 1; i >= 0; i--)
//             {
//                 Bullet bullet = _bullets[i];
//                 Vector3 moveStep = bullet.Direction * (bullet.Speed * Time.fixedDeltaTime);
//                 bullet.transform.position += moveStep;
//
//                 if (!_levelBounds.InBounds(bullet.transform.position))
//                 {
//                     _bullets.RemoveAt(i);
//
//                     bullet.OnHit -= this.OnTriggerEntered;
//                     bullet.gameObject.SetActive(false);
//                     _pool.Push(bullet);
//                 }
//             }
//         }
//
//         public void Spawn(Vector2 position, Vector2 direction, float speed, int damage, TeamType team)
//         {
//             if (_pool.TryPop(out Bullet bullet))
//                 bullet.gameObject.SetActive(true);
//             else
//                 bullet = Instantiate(_prefab, _container);
//             
//            
//             bullet.SetDirection(direction);
//             
//
//             bullet.transform.position = position;
//             bullet.transform.rotation = Quaternion.LookRotation(direction, Vector3.forward);
//             bullet.gameObject.layer = team switch
//             {
//                 TeamType.None => LayerMask.NameToLayer("Default"),
//                 TeamType.Player => LayerMask.NameToLayer("PlayerBullet"),
//                 TeamType.Enemy => LayerMask.NameToLayer("EnemyBullet"),
//                 _ => throw new ArgumentOutOfRangeException(nameof(team), team, null)
//             };
//             
//             bullet.gameObject.SetActive(true);
//             bullet.BulletVFX.Play();
//             
//             bullet.OnHit += this.OnTriggerEntered;
//             _bullets.Add(bullet);
//         }
//
//         private void OnTriggerEntered(Bullet bullet, Collider2D other)
//         {
//             if (!other.TryGetComponent(out ShipController ship)) 
//                 return;
//
//             if (bullet.Team == TeamType.Player && ship is Enemy ||
//                 bullet.Team == TeamType.Enemy && ship is PlayerShip)
//             {
//                 // Deal damage to target:
//                 if (bullet.Damage > 0)
//                 {
//                     ship.currentHealth = Mathf.Clamp(ship.currentHealth - bullet.Damage, 0, ship.config.Health);
//                     ship.NotifyAboutHealthChanged(ship.currentHealth);
//  
//                     if (ship.currentHealth <= 0)
//                     {
//                         ship.NotifyAboutDead();
//                         ship.gameObject.SetActive(false);
//                     }
//                 }
//
//                 bullet.OnHit -= this.OnTriggerEntered;
//
//                 _bullets.Remove(bullet);
//
//                 bullet.BulletVFX.Stop();
//                 bullet.gameObject.SetActive(false);
//                 
//                 _pool.Push(bullet);
//
//                 // Explosion Vfx
//                 ParticleSystem prefab = _configView.ExplosionVFX;
//                 Instantiate(prefab, bullet.transform.position, prefab.transform.rotation);
//             }
//         }
//     }
// }