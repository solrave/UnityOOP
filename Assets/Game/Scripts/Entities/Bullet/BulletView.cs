using System.Collections;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class BulletView : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _bodyBlue;
        [SerializeField] private ParticleSystem _bodyRed;

        [SerializeField] private ParticleSystem _explosionBlue;
        [SerializeField] private ParticleSystem _explosionRed;

        private ParticleSystem _currentBody;
        private ParticleSystem _currentExplosion;
        private Bullet _bullet;

        [Inject]
        public void Construct(Bullet bullet)
        {
            _bullet = bullet;
        }

    private void OnEnable()
        {   
            SelectCurrentRepresentation();
            PlayVisual();
            _bullet.OnHit += PlayExplosion;
        }
        
        private void OnDisable()
        {
            _bullet.OnHit -= PlayExplosion;
        }
        
        private void SelectCurrentRepresentation()
        {
            switch (_bullet.Team)
            {
                case TeamType.Player:
                    _currentBody = _bodyBlue;
                    _currentExplosion = _explosionBlue;
                    _bodyRed.gameObject.SetActive(false);
                    _explosionRed.gameObject.SetActive(false);
                    break;
                
                case TeamType.Enemy:
                    _currentBody = _bodyRed;
                    _currentExplosion = _explosionRed;
                    _bodyBlue.gameObject.SetActive(false);
                    _explosionBlue.gameObject.SetActive(false);
                    break;
            }
        }
        
        private void PlayVisual()
        {
           _currentBody.gameObject.SetActive(true);
           _currentBody.Play();
        }

        private void PlayExplosion()
        {
            _currentBody.Stop();
            _currentExplosion.gameObject.SetActive(true);
            _currentExplosion.Play();
            StartCoroutine(WaitForExplosion());
        }

        private IEnumerator WaitForExplosion()
        {
            yield return new WaitForSeconds(_currentExplosion.main.duration);
                _bullet.IsExpired();
        }
    }
}