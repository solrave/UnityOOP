using System;
using Modules.UI;
using UnityEngine;

namespace Game.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField]
        private Ship _playerShip;

        [SerializeField]
        private EnemyShipSpawner enemyShipSpawner;
        
        [SerializeField]
        private ScoreView _scoreView;

        [SerializeField]
        private HealthView _healthView;

        [SerializeField] 
        private GameOverView _gameOverView;
        
        private void OnEnable()
        {
            _playerShip.OnShipDestroyed += ShowGameOver;
            _playerShip.OnHealthChanged += SetHealth;
            enemyShipSpawner.OnShipDespawned += SetScore;
        }
        
        private void OnDisable()
        {
            _playerShip.OnShipDestroyed -= ShowGameOver;
            _playerShip.OnHealthChanged -= SetHealth;
            enemyShipSpawner.OnShipDespawned -= SetScore;
        }

        private void SetScore()
        {
            _scoreView.SetValue();
        }
        
        private void SetHealth(int health, int maxHealth)
        {
            _healthView.SetHealth(health, maxHealth);
        }

        private void ShowGameOver(Transform ship)
        {
            _gameOverView.Show();
        }
    }
}