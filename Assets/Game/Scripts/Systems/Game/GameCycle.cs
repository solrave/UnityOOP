using System;
using Game.Scripts.Components;
using Game.Scripts.Components.Core;
using Game.Scripts.Context.GameObject.Ship;
using UnityEngine;
using Zenject;

namespace Game
{
    public class GameCycle : MonoBehaviour
    {
        public event Action OnGameOver;
        
        private Entity _player;

        [Inject]
        public void Construct(Entity player)
        {
            _player = player;
        }

        private void OnEnable()
        {
            _player.Get<IHealthComponent>().OnHealthDepleted += StopGame;
        }

        private void OnDisable()
        {
            _player.Get<IHealthComponent>().OnHealthDepleted -= StopGame;
        }
        
        private void StopGame()
        {
            OnGameOver?.Invoke();
        }
    }
}