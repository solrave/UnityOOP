using System;
using UnityEngine;

namespace Game.Components
{
    public class FireComponent : MonoBehaviour
    {
        public event Action FireAnimationRequested;
        public TeamType Team { get; private set; }
        
        [field: SerializeField]
        public Transform FirePoint { get; private set; }
        
        [SerializeField] 
        private float _fireCooldown = 0.25f;
        
        private float _fireTime;
        
        public void Fire()
        {
            float time = Time.time;
            
            if (time - _fireTime < _fireCooldown)
                return;

            FireAnimationRequested?.Invoke();
            _fireTime = time;
        }
    }
}