using DG.Tweening;
using Game.Scripts.Context.GameObject.Ship;
using Modules.Utils;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class ShipView : MonoBehaviour
    {
        [SerializeField] private Transform _visualTransform;

        [SerializeField] private float _moveRotationAngle;

        [SerializeField] private AudioSource _audioSource;

        [SerializeField] private AnimationCurve _hitAnimationCurve;

        [SerializeField] private float _hitDuration;

        [SerializeField] private ParticleSystem _muzzleVFX;

        [SerializeField] private ParticleSystem _destroyVFX;

        [SerializeField] private AudioClip _shotSFX;

        [SerializeField] private AudioClip _damageSFX;

        [SerializeField] private Material _material;

        [SerializeField] private CameraShaker _cameraShaker;
        
        private readonly string _hitPropertyName = "_HitBlend";
        private Tweener _renderer;
        private Tweener _damageAnimation;
        private IHealthComponent _healthComponent;
        private IFireComponent _fireComponent;
        private IMoveComponent _moveComponent;
        
        [Inject]
        public void Construct(IHealthComponent healthComponent, IFireComponent fireComponent, IMoveComponent moveComponent)
        {
            _healthComponent = healthComponent;
            _fireComponent = fireComponent;
            _moveComponent = moveComponent;
        }

        private void OnEnable()
        {
            _fireComponent.OnFire += AnimateFire;
            _healthComponent.OnDestroyAnimationRequested += AnimateDestruction;
            _healthComponent.OnHit += AnimateDamage;
            _moveComponent.OnMove += AnimateMovement;
        }

        private void OnDisable()
        {
            _fireComponent.OnFire -= AnimateFire;
            _healthComponent.OnDestroyAnimationRequested -= AnimateDestruction;
            _healthComponent.OnHit -= AnimateDamage;
            _moveComponent.OnMove -= AnimateMovement;
        }

        public void AnimateFire()
        { 
            PlaySound(_shotSFX);
            PlayEffect(_muzzleVFX);
        }

        public void AnimateDamage()
        { 
            if (_damageAnimation.IsActive())
                _damageAnimation.Kill();
            
            _damageAnimation = DOVirtual.Float(
                0f,
                1f,
                _hitDuration,
                progress => _material?.SetFloat(_hitPropertyName,
                    _hitAnimationCurve.Evaluate(progress))
            );
            
           PlaySound(_damageSFX);
        }

        public void AnimateDestruction()
        {
           PlayEffect(_destroyVFX);
        }

        public void AnimateMovement(Vector2? inputDirection,float speed)
        {
            Vector3 shipAngles = _visualTransform.localEulerAngles;
            
            if (inputDirection is not null)
            {
                shipAngles.x = _moveRotationAngle * inputDirection.Value.y;
                shipAngles.y = _moveRotationAngle / 2 * inputDirection.Value.x * -1f;
            }
            
            Quaternion shipRotation = Quaternion.Euler(shipAngles);
            float t = speed * Time.deltaTime;
            _visualTransform.localRotation 
                = Quaternion.Lerp(_visualTransform.localRotation, shipRotation, t);
        }
        
        private void PlayEffect(ParticleSystem effect)
        {
            if (effect)
                Instantiate(effect, _visualTransform.position, effect.transform.rotation);
        }

        private void PlaySound(AudioClip clip)
        {
            if (clip)
                this._audioSource.PlayOneShot(clip);
        }
    }
}