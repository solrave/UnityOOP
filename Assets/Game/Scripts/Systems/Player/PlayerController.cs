using System;
using Modules.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private Ship _playerShip;
        
        private void Update() => ListenInput();
        
        private void ListenInput()
        {
            Vector2? direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            
            if (direction != Vector2.zero)
            {
               _playerShip.SetDirection(direction);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
               _playerShip.Fire();
            }
        }
    }
}