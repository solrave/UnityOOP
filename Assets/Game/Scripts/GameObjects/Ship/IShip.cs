using System;
using UnityEngine;

namespace Game
{
    public interface IShip
    {
        public event Action<Vector2> OnShipDestroyed;
        public event Action OnDamageTaken;
        
        public bool ReadyToShoot { get; }

        public Vector2 Position  { get; }

        public void Fire();
        public void FireAt(Vector2 targetPosition);
        public void SetDirection(Vector2? position);
        public void SetPosition(Vector2 position);
        public void TakeDamage(int damage);
    }
}