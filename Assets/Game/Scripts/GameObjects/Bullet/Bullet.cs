using System;
using Game.Scripts.Components;
using UnityEngine;

namespace Game
{
    public class Bullet : MonoBehaviour
    {
        public event Action<Bullet> OnHit;
        
        [field: SerializeField]
        public TeamType Team { get; private set; }

        [SerializeField] 
        private MoveComponent _moveComponent;
        
        [SerializeField] 
        private BulletAnimationComponent _animationComponent;

        [SerializeField] 
        private BulletDamageComponent _damageComponent;

        public void SetDirection(Vector2 direction) => _moveComponent.SetSimpleDirection(direction);

        private void Awake()
        {
            _animationComponent.PlayVisual();
        }

        private void FixedUpdate()
        {
            _moveComponent.Move();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out Ship ship))
                return;

            {
                if (this._damageComponent.Damage > 0)
                {
                    ship.TakeDamage(_damageComponent.Damage);
                }
                
                _animationComponent.StopVisual();
                this.gameObject.SetActive(false);

                OnHit?.Invoke(this);

               // Instantiate(ExplosionVFX, this.transform.position, this.transform.rotation);
            }
        }
    }
}