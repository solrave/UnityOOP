using System;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class GameController : IInitializable, IDisposable
    {
        private readonly Entity _player;
        private readonly ShipManager _shipManager;
        private readonly PlayerController _playerController;

        public GameController(CharacterProvider provider, ShipManager shipManager, PlayerController playerController)
        {
            _player = provider.Player;
            _shipManager = shipManager;
            _playerController = playerController;
        }
        
        public void Initialize()
        {
            _player.Get<IHealthComponent>().OnShipDestroyed += StopGame;
        }

        public void Dispose()
        {
            _player.Get<IHealthComponent>().OnShipDestroyed -= StopGame;
        }

        private void StopGame()
        {
            _shipManager.StopAllShips();
            _shipManager.StopSpawn();
            _playerController.StopControl();
        }
    }
}            