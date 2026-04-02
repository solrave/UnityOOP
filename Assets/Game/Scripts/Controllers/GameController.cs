using System;
using Game.UI;
using UnityEngine;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] 
        private Ship _playerShip;

        [SerializeField] 
        private UIController _uiController;

        [SerializeField] 
        private ShipSpawner _shipSpawner;

        private void OnEnable()
        {
            _playerShip.OnShipDestroyed += StopGame;
            _playerShip.OnShipDestroyed += _uiController.ShowGameOver;
            _playerShip.OnHealthChanged += _uiController.SetHealth;
            _shipSpawner.OnShipDespawned += _uiController.SetScore;
        }

        private void StopGame(Ship ship)
        {
            ship.gameObject.SetActive(false);
            _shipSpawner.StopAllShips();
        }

        private void OnDisable()
        {
            _playerShip.OnShipDestroyed -= StopGame;
            _playerShip.OnShipDestroyed -= _uiController.ShowGameOver;
            _playerShip.OnHealthChanged -= _uiController.SetHealth;
            _shipSpawner.OnShipDespawned -= _uiController.SetScore;
        }
    }
}