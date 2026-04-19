using System;
using Game.Scripts.GameObjects.Components;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class EnemyShipAI : MonoBehaviour
    {
        public event Action<EnemyShipAI> OnShipDestroyed;
        
        [SerializeField]
        public Ship _enemyShip;
        
        [SerializeField]
        private FollowComponent _followComponent;
        
        private Vector2 _firePosition;
        private Ship _target;

        private void OnEnable()
        {
            _enemyShip.OnShipDestroyed += ShipDestroyed;
        }
        
        private void OnDisable()
        {
            _enemyShip.OnShipDestroyed -= ShipDestroyed;
        }
        
        private void FixedUpdate()
        {
            var targetDirection = _followComponent.GetDirection(_firePosition, this.transform.position);
            _enemyShip.SetDirection(targetDirection);
            
            if (_followComponent.IsReached && _enemyShip.ReadyToShoot && _target )
            {
                _enemyShip.FireAt(_target.Position);
            }
        }
        
        public void SetFirePosition(Vector2 position) => _firePosition = position;
        
        public void SetPosition(Vector2 position) => _enemyShip.SetPosition(position);
        
        public void SetTarget(Ship ship) => _target = ship;
        
        public void SetSpawner(BulletSpawner spawner) => _enemyShip.SetSpawner(spawner);

        private void ShipDestroyed(Transform obj)
        {
            OnShipDestroyed?.Invoke(this);
            this.gameObject.SetActive(false);
        }
    }
}
