using Modules.Utils;
using UnityEngine;

namespace Game
{
    public class PositionClamper : MonoBehaviour
    {
        [SerializeField]
        private Ship _playerShip;
        
        [SerializeField] 
        private LevelBounds _playerBounds;
        
        private void LateUpdate() => ClampInLevelBounds();
        
        private void ClampInLevelBounds()
        {
            if (!_playerBounds.InBounds(_playerShip.Position))
            {
                var newPosition = _playerBounds.ClampInBounds(_playerShip.Position);
                _playerShip.SetPosition(newPosition);
            }
        }
    }
}