using System;
using Modules.UI;
using UnityEngine;

namespace Game.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField]
        private ScoreView _scoreView;

        [SerializeField]
        private HealthView _healthView;

        [SerializeField] 
        private GameOverView _gameOverView;

        public void SetScore()
        {
            _scoreView.SetValue();
        }
        
        public void SetHealth(int health, int maxHealth)
        {
            _healthView.SetHealth(health, maxHealth);
        }

        public void ShowGameOver(Ship ship)
        {
            _gameOverView.Show();
        }
    }
}