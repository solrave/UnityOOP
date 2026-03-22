using UnityEngine;

namespace Game.Scripts.Components
{
    public class BulletAnimationComponent : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _bulletVFX;

        [SerializeField] private ParticleSystem _explosionVFX;

        public void PlayVisual()
        {
            _bulletVFX.Play();
        }

        public void StopVisual()
        {
            _bulletVFX.Stop();
        }
        
        public void PlayExplosion()
        {
            _explosionVFX.Play();
        }
    }
}