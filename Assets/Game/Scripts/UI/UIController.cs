using System;
using Game.Scripts.Components;
using Game.Scripts.Components.Core;
using Game.Scripts.Context.GameObject.Ship;
using Modules.UI;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class UIController : MonoBehaviour
    {
        private Entity _player;

        [SerializeField]
        private ScoreView _scoreView;

        [SerializeField]
        private HealthView _healthView;

        [SerializeField] 
        private GameOverView _gameOverView;
        
        [Inject]
        public void Construct(Entity player)
        {
            _player = player;
        }
        
        private void OnEnable()
        {
            _player.Get<IHealthComponent>().OnHealthDepleted += ShowGameOver;
            _player.Get<IHealthComponent>().OnHealthChanged += SetHealth;
        }
        
        private void OnDisable()
        {
            _player.Get<IHealthComponent>().OnHealthDepleted -= ShowGameOver;
            _player.Get<IHealthComponent>().OnHealthChanged -= SetHealth;
        }

        private void SetScore()
        {
            _scoreView.SetValue();
        }
        
        private void SetHealth(int health, int maxHealth)
        {
            _healthView.SetHealth(health, maxHealth);
        }

        private void ShowGameOver()
        {
            _gameOverView.Show();
        }
    }
}