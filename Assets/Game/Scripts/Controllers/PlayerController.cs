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

        private Rigidbody2D _rigidbody;

        private void OnEnable()
        {
            _rigidbody = _playerShip.GetComponent<Rigidbody2D>();
           OnMoveInputReceived += _playerShip.SetDestination;
           OnFireInputReceived += _playerShip.Fire;
        }

        private void OnDisable()
        { 
            OnMoveInputReceived -= _playerShip.SetDestination;
            OnFireInputReceived -= _playerShip.Fire;
        }

        private void Update() => ListenInput();

        private void LateUpdate() => ClampInLevelBounds();


        private void ClampInLevelBounds()
        {
            if (!_playerBounds.InBounds(_playerShip.transform.position))
            {
                _rigidbody.position = _playerBounds.ClampInBounds(_playerShip.transform.position);
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