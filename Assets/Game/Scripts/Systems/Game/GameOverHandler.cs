using System;
using Zenject;

namespace Game.Gameplay
{
    public class GameOverHandler : IInitializable, IDisposable
    {
        private readonly EnemyManager _enemyManager;
        private readonly PlayerController _playerController;
        private readonly GameCycle _gameCycle;

        public GameOverHandler(EnemyManager enemyManager,
            PlayerController playerController,
            GameCycle gameCycle)
        {
            _enemyManager = enemyManager;
            _playerController = playerController;
            _gameCycle = gameCycle;
        }

        public void Initialize()
        {
            _gameCycle.OnGameFinished += this.HandleGameOver;
        }

        public void Dispose()
        {
            _gameCycle.OnGameFinished -= this.HandleGameOver;
        }
        
        private void HandleGameOver()
        {
            _enemyManager.StopAllShips();
            _enemyManager.StopSpawn();
            _playerController.StopControl();
        }
    }
}