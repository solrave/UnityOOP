using System;
using Game.Scripts.Components;
using Game.Scripts.Components.Core;
using Game.Scripts.Context.GameObject.Ship;
using Game.Scripts.Context.GameObject.Ship.Player;
using UnityEngine;
using Zenject;

namespace Game
{
    public class PlayerController : ITickable
    {
        private readonly PlayerEntity _player;
        
        public PlayerController(PlayerEntity player)
        {
            _player = player;
        }

        public void Tick() => ListenInput();
        
        private void ListenInput()
        {
            Vector2? direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            
            if (direction != Vector2.zero)
            {
               _player.Get<IMoveComponent>().SetDirection(direction);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
               _player.Get<IFireComponent>().FireUp();
            }
        }
    }
}