using Game.Gameplay;
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
        private GameOverScreen _gameOverScreen;
        
        [Inject]
        public void Construct(CharacterProvider provider)
        {
            _player = provider.Player;
        }
        
        private void OnEnable()
        {
            _player.Get<IHealthComponent>().OnShipDestroyed += ShowGameOver;
            _player.Get<IHealthComponent>().OnHealthChanged += SetHealth;
        }
        
        private void OnDisable()
        {
            _player.Get<IHealthComponent>().OnShipDestroyed -= ShowGameOver;
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
            _gameOverScreen.Show();
        }
    }
}