using System;
using UnityEngine;

namespace Game.Scripts.GameObjects.Components
{
    public class FollowComponent : MonoBehaviour
    {
        public bool IsReached { get; private set; }

        [SerializeField] 
        private float _stoppingDistance;

        [SerializeField]
        private MoveComponent _moveComponent;
        
        private Transform _target;
        private Vector2 _destination;

        public void SetDestination(Vector2 destination) => _destination = destination;

        private void FixedUpdate() => Follow();

        private void Follow()
        {
            Vector2 distance = _destination - (Vector2) transform.position;
            IsReached = distance.sqrMagnitude < _stoppingDistance;
            
            Vector2? moveDirection = IsReached ? null : distance.normalized;

            _moveComponent.SetDirection(moveDirection);
            
        }
    }
}