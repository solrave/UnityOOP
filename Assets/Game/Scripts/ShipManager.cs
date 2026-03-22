// using System;
// using System.Collections.Generic;
// using Game.Services;
// using UnityEngine;
//
// namespace Game
// {
//     // +
//     public sealed class ShipManager
//     {
//         private Ship _targetPlayer;
//         private List<Ship> _activeEnemies;
//         
//         [Inject]
//         private ShipSpawner _shipSpawner;
//         
//         [Inject]
//         private EnemyShipController _enemyController;
//         
//         public void RunEnemyBehaviour()
//         {
//             if (this.currentHealth <= 0 || this._target == null || this._target.currentHealth <= 0)
//                 return;
//
//             Vector2 distance = _destination - (Vector2) this.transform.position;
//             bool isNotReached = distance.sqrMagnitude > _stoppingDistance * _stoppingDistance;
//             
//             moveDirection = isNotReached ? distance.normalized : Vector3.zero;
//
//             if (isNotReached)
//             {
//                 moveComponent.SetDirection(distance.normalized);
//             }
//            
//         }
//     }
// }