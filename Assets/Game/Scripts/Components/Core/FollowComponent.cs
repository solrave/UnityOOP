using System;
using UnityEngine;

namespace Game.Scripts.Components.Core
{
    public class FollowComponent : IFollowComponent
    {
        [Serializable]
        public sealed class Settings
        {
            [field: SerializeField]
            public float StoppingDistance { get; private set; }
        }
        
        public bool IsReached { get; private set; }
        private readonly Settings _settings;

        public FollowComponent(Settings settings)
        {
            _settings = settings;
        }
       
        public Vector2? GetDirection(Vector2? target,Vector2 position)
        {
            
            if (!target.HasValue) return null;
            
            Vector2? distance = target - position;
            IsReached = distance.Value.sqrMagnitude < _settings.StoppingDistance * _settings.StoppingDistance;
            
            Vector2? moveDirection = IsReached ? null : distance.Value.normalized;
            return moveDirection;
        }
    }

    public interface IFollowComponent
    {
        public bool IsReached { get; }
        public Vector2? GetDirection(Vector2? target, Vector2 position);
    }
}