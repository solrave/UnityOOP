using System;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class GameController : IInitializable, IDisposable
    {
        private Entity _player;
        private ShipManager _shipSpawner;

        public GameController(CharacterProvider provider, ShipManager shipSpawner)
        {
            _player = provider.Player;
            _shipSpawner = shipSpawner;
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
            _shipSpawner.StopAllShips();
        }
    }
}            