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

        private EnemyManager _manager;

        [Inject]
        public void Construct(CharacterProvider provider, EnemyManager manager)
        {
            _player = provider.Player;
            _manager = manager;
        }
        
        private void OnEnable()
        {
            _manager.OnKillCountIncrease += SetScore;
            _player.Get<IHealthComponent>().OnHealthEmpty += ShowGameOver;
            _player.Get<IHealthComponent>().OnHealthChanged += SetHealth;
        }
        
        private void OnDisable()
        {
            _manager.OnKillCountIncrease -= SetScore;
            _player.Get<IHealthComponent>().OnHealthEmpty -= ShowGameOver;
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