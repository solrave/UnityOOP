using Game.Scripts.Components;
using Game.Scripts.Components.Core;
using Game.Scripts.Context.GameObject.Ship;
using UnityEngine;
using Modules.Utils;
using Zenject;

namespace Game
{
    public class PlayerClamper : ILateTickable
    {
        private Entity _player;
        private LevelBounds _playerBounds;
        
        public PlayerClamper(Entity player, LevelBounds playerBounds)
        {
            _player = player;
            _playerBounds = playerBounds;
        }

        public void LateTick() => ClampInLevelBounds();
        
        private void ClampInLevelBounds()
        {
            if (!_playerBounds.InBounds(_player.Get<IMoveComponent>().Position))
            {
                var newPosition = _playerBounds.ClampInBounds(_player.Get<IMoveComponent>().Position);
                _player.Get<IMoveComponent>().SetPosition(newPosition);
            }
        }
    }
}