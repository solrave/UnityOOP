using Game.Scripts.Components;
using Game.Scripts.Components.Core;
using Game.Scripts.Context.GameObject.Ship;
using Game.Scripts.Context.GameObject.Ship.Player;
using UnityEngine;
using Modules.Utils;
using Zenject;

namespace Game
{
    public class PositionClamper : ILateTickable
    {
        private PlayerEntity _player;
        private LevelBounds _playerBounds;
        
        public PositionClamper(PlayerEntity player, LevelBounds playerBounds)
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