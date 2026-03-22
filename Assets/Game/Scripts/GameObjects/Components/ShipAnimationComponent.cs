using DG.Tweening;
using Modules.Utils;
using UnityEngine;

namespace Game.Scripts.Components
{
    public class ShipAnimationComponent : MonoBehaviour
    {
        private Tweener _damageAnimation;

        [SerializeField]
        public AudioSource audioSource;
        
        [SerializeField]
        public AnimationCurve hitAnimationCurve;
        
        [SerializeField]
        public float hitDuration = 0.2f;
        
        [SerializeField]
        public MeshRenderer renderer;
        
        [SerializeField]
        public ParticleSystem fireVFX;

        [SerializeField]
        public AudioClip fireSfx;
        
        [SerializeField]
        public AudioClip damageSfx;

        [SerializeField]
        public ParticleSystem destroyEffectPrefab;
        
        [SerializeField]
        public Material material;
        
        private CameraShaker _cameraShaker;
        private readonly string _hitPropertyName = "_HitBlend";

        public void AnimateFire()
        {
            if (fireSfx)
                audioSource.PlayOneShot(fireSfx);

            if (fireVFX)
                fireVFX.Play();
        }

        public void AnimateDamage()
        {
            if (_damageAnimation.IsActive())
                _damageAnimation.Kill();

            _damageAnimation = DOVirtual.Float(
                0f,
                1f,
                hitDuration,
                progress => material?.SetFloat(_hitPropertyName,
                    hitAnimationCurve.Evaluate(progress))
            ).SetLink(renderer.gameObject);

            if (damageSfx)
                audioSource.PlayOneShot(damageSfx);
            _cameraShaker.Shake();
        }

        public void AnimateDestruction(Ship ship)
        {
            _cameraShaker.Shake();
            ParticleSystem prefab = destroyEffectPrefab;
            GameObject.Instantiate(prefab, ship.transform.position, prefab.transform.rotation);
        }
    }
}