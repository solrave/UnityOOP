using System;
using Modules.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class PlayerController : MonoBehaviour
    {
        public event Action OnFireInputReceived;
        public event Action<Vector2?> OnMoveInputReceived;
        
        [SerializeField]
        private Ship _playerShip;
        
        [SerializeField] 
        private LevelBounds _playerBounds;

        [SerializeField]
        private BulletSpawner _bulletSpawner;

        private void OnEnable()
        {
           OnMoveInputReceived += _playerShip.SetMoveDirection;
           OnFireInputReceived += _playerShip.Fire;
           _playerShip.OnFire += _bulletSpawner.Spawn;
        }

        private void OnDisable()
        { 
            OnMoveInputReceived -= _playerShip.SetMoveDirection;
            OnFireInputReceived -= _playerShip.Fire;
           _playerShip.OnFire -= _bulletSpawner.Spawn;
            
        }

        private void Update() => ListenInput();

        private void LateUpdate() => ClampInLevelBounds();


        private void ClampInLevelBounds()
        {
            if (!_playerBounds.InBounds(_playerShip.transform.position))
            {
                _playerShip.transform.position = _playerBounds.ClampInBounds(_playerShip.transform.position);
            }
        }

        private void ListenInput()
        {
            Vector2? direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            
            if (direction != Vector2.zero)
            {
                OnMoveInputReceived?.Invoke(direction);
            }

            if (Input.GetKey(KeyCode.Space))
            {
                OnFireInputReceived?.Invoke();
            }
        }
    }
}