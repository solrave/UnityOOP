using UnityEngine;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] 
        private GameCycle _cycle;

        [SerializeField]
        private EnemyShipSpawner enemyShipSpawner;
        
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
            enemyShipSpawner.StopAllShips();
        }

    }
}            