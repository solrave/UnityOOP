using System;
using Game.Scripts.Context.GameObject.Ship;
using Game.Scripts.Entities.Components;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class EnemyAI : IFixedTickable
    {
        public event Action<EnemyAI> OnDispose;
        
        private Entity _target;
        private Vector2 _firePosition;
        private IMoveComponent _moveComponent;
        private IFireComponent _fireComponent;
        private RigidbodyComponent _rigidbodyComponent;
        private AISettings _settings;
        private bool _isReached;
        
        public EnemyAI(CharacterProvider target, IMoveComponent moveComponent,
            IFireComponent fireComponent, AISettings settings, RigidbodyComponent rigidbodyComponent)
        {
            _moveComponent = moveComponent;
            _fireComponent = fireComponent;
            _settings = settings;
            _rigidbodyComponent = rigidbodyComponent;
            _target = target.Player;
        }
        
        public void FixedTick()
        {
            if (!_target) return;
            
            Vector2? distance = _target.Get<RigidbodyComponent>().Position - _rigidbodyComponent.Position;
            _isReached = distance.Value.sqrMagnitude < _settings.stoppingDistance * _settings.stoppingDistance;
            
           if(!_isReached)
           {
                _moveComponent.SetDirection(distance.Value.normalized);
           }
           else
           {
               _fireComponent.FireAt(_target.Get<IMoveComponent>().Position);
           }
        }
    }
}
