using System;
using DG.Tweening;
using Modules.Utils;
using UnityEngine;

namespace Game.Scripts.Components
{
    public class AnimationComponent : MonoBehaviour
    {
        private Tweener _damageAnimation;

        [SerializeField]
        public AudioSource audioSource;
        
        [SerializeField]
        public AnimationCurve hitAnimationCurve;
        
        [SerializeField]
        public float hitDuration = 0.2f;
        
        [SerializeField]
        private ParticleSystem _fireVFX;

        [SerializeField]
        private AudioClip _fireSFX;
        
        [SerializeField]
        private AudioClip _damageSfx;

        [SerializeField]
        private ParticleSystem _destroyEffectPrefab;
        
        [SerializeField]
        private Material _material;
        
        [SerializeField]
        private CameraShaker _cameraShaker;
        
        private readonly string _hitPropertyName = "_HitBlend";
        private Tweener _renderer;

        private void OnEnable()
        {
           //_renderer = GetComponent<Renderer>();
        }

        public void AnimateFire(Transform position)
        {
            if (_fireSFX)
                audioSource.PlayOneShot(_fireSFX);

            if (_fireVFX)
            {
                Instantiate(_fireVFX, position);
                _fireVFX.Play();
            }
        }

        public void AnimateDamage()
        { 
            if (_damageAnimation.IsActive())
                _damageAnimation.Kill();
            
            _damageAnimation = DOVirtual.Float(
                0f,
                1f,
                hitDuration,
                progress => _material?.SetFloat(_hitPropertyName,
                    hitAnimationCurve.Evaluate(progress))
            );
            
            if (_damageSfx)
                audioSource.PlayOneShot(_damageSfx);
            
            //_cameraShaker.Shake();
        }

        public void AnimateDestruction(Ship ship)
        {
            //  _cameraShaker.Shake();
            ParticleSystem prefab = _destroyEffectPrefab;
            Instantiate(prefab, ship.transform.position, prefab.transform.rotation);
        }
    }
}