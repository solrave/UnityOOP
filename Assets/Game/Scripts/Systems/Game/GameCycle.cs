using System;

namespace Game.Gameplay
{
    public interface IGameCycle
    {
        event Action OnGameFinished;
        void FinishGame();
    }
    
    public class GameCycle : IGameCycle
    {
        public event Action OnGameFinished;
        
        public void FinishGame()
        {
            OnGameFinished?.Invoke();
        }
    }
}