using System;
using UnityEngine;

namespace Game.Gameplay
{
    public class CollisionListener : MonoBehaviour
    {
        public event Action<Collision2D> OnCollision;

        private void OnCollisionEnter2D(Collision2D other)
        {
            this.OnCollision?.Invoke(other);
        }
    }
}