// using Game.Scripts.GameObjects.Ship;
// using Game.Scripts.Systems.Enemy.Spawn.Points;
//
// namespace Game.Scripts.Systems.Enemy.Spawn.ArgsProvider
// {
//     public class EnemyCreateArgsProvider
//     {
//         private readonly FirePointService _firePointService;
//         private readonly SpawnPointService _spawnPointService;
//         private readonly PlayerEntity _target;
//         
//         public EnemyCreateArgsProvider(PlayerEntity target
//             ,FirePointService firePointService, SpawnPointService spawnPointService)
//         {
//             _target = target;
//             _firePointService = firePointService;
//             _spawnPointService = spawnPointService;
//         }
//         
//         public EnemyAI.Settings GetNewArgs()
//         {
//             var shipAIArgs = new EnemyAI.Settings()
//             {
//                 startPosition = _spawnPointService.GetSpawnPoint().Position,
//                 firePosition = _firePointService.GetFirePosition().Position,
//                 target = _target
//             };
//             
//             return shipAIArgs;
//         }
//     }
// }