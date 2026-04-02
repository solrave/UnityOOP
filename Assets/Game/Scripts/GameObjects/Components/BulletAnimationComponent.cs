using UnityEngine;

namespace Game.Scripts.Components
{
    public class BulletAnimationComponent : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _bulletVFX;

        [SerializeField] private ParticleSystem _explosionVFX;

        public void PlayVisual()
        {
            _bulletVFX.Clear();
            _bulletVFX.Play();
        }

        public void StopVisual()
        {
            _bulletVFX.Stop();
        }
        
        public void PlayExplosion(Bullet bullet)
        {
            var explode = Instantiate(_explosionVFX, bullet.transform.position, bullet.transform.rotation);
            
            if (explode != null)
                explode.Play();
        }
    }
}