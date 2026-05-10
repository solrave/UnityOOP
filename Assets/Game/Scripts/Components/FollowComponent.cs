using System;
using UnityEngine;

namespace Game.Scripts.GameObjects.Components
{
    [Serializable]
    public class FollowComponent : IFollowComponent
    {
        public bool IsReached { get; private set; }
        private float _stoppingDistance;

        public FollowComponent(float stoppingDistance)
        {
            _stoppingDistance = stoppingDistance;
        }
       
        public Vector2? GetDirection(Vector2? target,Vector2 position)
        {
            Vector2? distance = target - position;
            
            if (IsReached && distance is null) return null;
            
            IsReached = distance.Value.sqrMagnitude < _stoppingDistance * _stoppingDistance;
            
            Vector2? moveDirection = IsReached ? null : distance.Value.normalized;
            return moveDirection;
        }
    }

    public interface IFollowComponent
    {
        
    }
}