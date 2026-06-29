using System;
using Zenject;

namespace Game.Gameplay
{
    public class GameController : IInitializable, IDisposable
    {
        private readonly Entity _player;
        private readonly IGameCycle _gameCycle;
        
        public GameController(CharacterProvider provider,
            IGameCycle gameCycle)
        {
            _gameCycle = gameCycle;
            _player = provider.Player;
        }
        
        public void Initialize()
        {
            _player.Get<IHealthComponent>().OnHealthEmpty += StopGame;
        }

        public void Dispose()
        {
            _player.Get<IHealthComponent>().OnHealthEmpty -= StopGame;
        }

        private void StopGame()
        {
            _gameCycle.FinishGame();
        }
    }
}            