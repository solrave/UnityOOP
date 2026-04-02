using Game.Scripts.GameObjects.Components;
using UnityEngine;

namespace Game
{
    public class EnemyShip : Ship
    {
        [SerializeField]
        private FollowComponent _followComponent;
        
        protected override void Proceed()
        {
            if (_followComponent.IsReached && _healthComponent.HasHealth && _fireComponent.HasTarget)
            {
                _fireComponent.FireAt();
            }
        }

        public void SetTarget(Transform target) => _fireComponent.SetTarget(target);
        public void SetFollow(Vector2 destination) => _followComponent.SetDestination(destination);

        public void SetSpawner(BulletSpawner spawner) => _fireComponent.SetSpawner(spawner);
       
    }
}
