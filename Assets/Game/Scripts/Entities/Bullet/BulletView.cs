using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class BulletView : MonoBehaviour, IInitializable, IDisposable
    {
        public event Action OnDestroy;
        [SerializeField] 
        private ParticleSystem _bodyBlue;
        
        [SerializeField] 
        private ParticleSystem _bodyRed;

        [SerializeField] 
        private ParticleSystem _explosionBlue;
        
        [SerializeField] 
        private ParticleSystem _explosionRed;

        [SerializeField] 
        private TrailRenderer _rendererBlue;
        
        [SerializeField] 
        private TrailRenderer _rendererRed;
        
        private Bullet _bullet;
        private ParticleSystem _currentBody;
        private ParticleSystem _currentExplosion;
        private TeamComponent _teamComponent;
        private TrailRenderer _currentRenderer;

        [Inject]
        public void Construct(Bullet bullet, TeamComponent teamComponent)
        {
            _bullet = bullet;
            _teamComponent = teamComponent;
        }
        
        public void Initialize()
        {
            _bullet.OnInitialized += EnableView;
            _bullet.OnHit += PlayExplosion;
        }

        public void Dispose()
        {
            _bullet.OnInitialized -= EnableView;
            _bullet.OnHit -= PlayExplosion;
        }

        public void EnableView()
        {   
            SelectCurrentRepresentation();
            PlayVisual();
        }

        private void SelectCurrentRepresentation()
        {
            switch (_teamComponent.Team)
            {
                case TeamType.Player:
                    _currentBody = _bodyBlue;
                    _currentExplosion = _explosionBlue;
                    _currentRenderer = _rendererBlue;
                    _bodyRed.gameObject.SetActive(false);
                    _explosionRed.gameObject.SetActive(false);
                    break;
                
                case TeamType.Enemy:
                    _currentBody = _bodyRed;
                    _currentExplosion = _explosionRed;
                    _currentRenderer = _rendererRed;
                    _bodyBlue.gameObject.SetActive(false);
                    _explosionBlue.gameObject.SetActive(false);
                    break;
                
                default: Debug.Log($"Bullet has no team!");
                    break;
            }
        }
        
        private void PlayVisual()
        {
            _currentRenderer.Clear();
            _currentBody.gameObject.SetActive(true);
            _currentRenderer.gameObject.SetActive(true);
            _currentBody.Play();
            _currentRenderer.emitting = true;
        }

        private void PlayExplosion()
        {
            _currentBody.Stop();
            _currentRenderer.emitting = false;
            _currentRenderer.Clear();
            _currentExplosion.gameObject.SetActive(true);
            _currentExplosion.Play();
            _bullet.OnHit -= PlayExplosion;
            StartCoroutine(WaitForExplosion());
            OnDestroy?.Invoke();
        }

        private IEnumerator WaitForExplosion()
        {
            yield return new WaitForSeconds(_currentExplosion.main.duration);
        }
    }
}