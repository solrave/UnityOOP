using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class PlayerController : ITickable
    {
        private readonly Entity _player;
        private PositionClamper _clamper;
        private bool _stopControl;
        
        public PlayerController(CharacterProvider provider,
            [Inject (Id = BindingID.PlayerPositionClamper)] PositionClamper clamper)
        {
            _clamper = clamper;
            _player = provider.Player;
            _stopControl = false;
        }

        public void Tick() => ListenInput();
        
        private void ListenInput()
        {
            if (_stopControl) return;
            
            Vector2? direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            
            if (direction != Vector2.zero)
               _player.Get<IMoveComponent>().SetDirection(direction);
            
            if (Input.GetKeyDown(KeyCode.Space))
               _player.Get<IFireComponent>().FireUp();
            
            _clamper.ClampInLevelBounds(_player);
        }

        public void StopControl()
        {
            _stopControl = true;
            _player.Get<ShipView>().gameObject.SetActive(false);
        }
    }
}