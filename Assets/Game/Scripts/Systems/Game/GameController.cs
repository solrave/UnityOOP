using System;
using UnityEngine;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] 
        private GameCycle _cycle;

        [SerializeField]
        private ShipSpawner _shipSpawner;
        
        private void OnEnable()
        {
            _cycle.OnGameOver += StopGame;
        }

        private void OnDisable()
        {
            _cycle.OnGameOver -= StopGame;
            
        }
        
        private void StopGame()
        {
            _shipSpawner.StopAllShips();
        }

    }
}            