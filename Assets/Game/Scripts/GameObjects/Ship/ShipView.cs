using System;
using DG.Tweening;
using Modules.Utils;
using UnityEngine;

namespace Game.Scripts.Components
{
    public class ShipView : MonoBehaviour
    {
        [SerializeField]
        private Transform _visualTransform;
        [SerializeField]
        private float _moveRotationAngle  = 30f;
        
        [SerializeField] 
        private Ship _ship;

        [SerializeField]
        public AudioSource audioSource;
        
        [SerializeField]
        public AnimationCurve hitAnimationCurve;
        
        [SerializeField]
        public float hitDuration = 0.2f;
        
        [SerializeField]
        private ParticleSystem _muzzleVFX;

        [SerializeField]
        private AudioClip _shotSFX;
        
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
        private Tweener _damageAnimation;

        private void OnEnable()
        {
            _ship.OnFire += AnimateFire;
            _ship.OnShipDestroyed += AnimateDestruction;
            _ship.OnDamageTaken += AnimateDamage;
            _ship.OnMove += AnimateMovement;
        }

        private void OnDisable()
        {
            _ship.OnFire -= AnimateFire;
            _ship.OnShipDestroyed -= AnimateDestruction;
            _ship.OnDamageTaken -= AnimateDamage;
            _ship.OnMove += AnimateMovement;
        }

        private void AnimateFire()
        {
            if (_shotSFX)
                audioSource.PlayOneShot(_shotSFX);

            if (_muzzleVFX)
            {
                _muzzleVFX.Play();
            }
        }

        private void AnimateDamage()
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
        }

        private void AnimateDestruction(Transform position)
        {
            ParticleSystem prefab = _destroyEffectPrefab;
            Instantiate(prefab, position.position, prefab.transform.rotation);
        }
        
        private void AnimateMovement(Vector2? inputDirection,float speed)
        {
            Vector3 shipAngles = _visualTransform.localEulerAngles;
            
            if (inputDirection is not null)
            {
                shipAngles.x = _moveRotationAngle * inputDirection.Value.y;
                shipAngles.y = _moveRotationAngle / 2 * inputDirection.Value.x * -1f;
            }
            
            Quaternion shipRotation = Quaternion.Euler(shipAngles);
            float t = speed * Time.deltaTime;
            _visualTransform.localRotation = Quaternion.Lerp(_visualTransform.localRotation, shipRotation, t);
        }
    }
}