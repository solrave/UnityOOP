using System;
using Game.UI;
using UnityEngine;

namespace Game
{
    public class GameCycle : MonoBehaviour
    {
        public event Action OnGameOver;
        
        [SerializeField]
        private Ship _ship;

        private void OnEnable()
        {
            _ship.OnShipDestroyed += StopGame;
        }

        private void OnDisable()
        {
            _ship.OnShipDestroyed -= StopGame;
        }
        
        private void StopGame(Vector2 ship)
        {
            OnGameOver?.Invoke();
        }
    }
}