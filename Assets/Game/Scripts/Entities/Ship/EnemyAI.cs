using System;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class EnemyAI : IFixedTickable
    {
        [Serializable]
        public class Settings
        {
            [SerializeField]
            public float stoppingDistance;
        }
        
        private Entity _target;
        private Vector2 _firePosition;
        private readonly IMoveComponent _moveComponent;
        private readonly IFireComponent _fireComponent;
        private readonly RigidbodyComponent _rigidbodyComponent;
        private readonly Settings _settings;
        private bool _isReached;
        
        public EnemyAI(CharacterProvider target, IMoveComponent moveComponent,
            IFireComponent fireComponent, Settings settings, RigidbodyComponent rigidbodyComponent)
        {
            _moveComponent = moveComponent;
            _fireComponent = fireComponent;
            _settings = settings;
            _rigidbodyComponent = rigidbodyComponent;
            _target = target.Player;
        }
        
        public void SetFirePosition(Vector2 firePosition) => _firePosition = firePosition;

        public void SetTarget(Entity entity) => _target = entity;
        
        public void FixedTick()
        {
            if (!_target) return;
            
            Vector2? distance = _firePosition - _rigidbodyComponent.Position;
            _isReached = distance.Value.sqrMagnitude < _settings.stoppingDistance * _settings.stoppingDistance;
            
           if(!_isReached)
           {
                _moveComponent.SetDirection(distance.Value.normalized);
           }
           else
           {
               var direction = _target.Get<RigidbodyComponent>().Position - _fireComponent.GunPoint;
               _fireComponent.FireAt(direction.normalized);
           }
        }
    }
}
