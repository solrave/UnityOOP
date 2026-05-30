using Game.Scripts.Context.GameObject.Ship;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class PlayerController : ITickable
    {
        private readonly Entity _player;
        
        public PlayerController(Entity player)
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