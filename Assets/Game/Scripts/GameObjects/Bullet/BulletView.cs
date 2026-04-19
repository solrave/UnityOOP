using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts.Components
{
    public class BulletView : MonoBehaviour
    {
        [SerializeField] private Bullet _bullet;
        
        [SerializeField] private ParticleSystem _bodyBlue;
        [SerializeField] private ParticleSystem _bodyRed;

        [SerializeField] private ParticleSystem _explosionBlue;
        [SerializeField] private ParticleSystem _explosionRed;

        private ParticleSystem _currentBody;
        private ParticleSystem _currentExplosion;

        private void OnEnable()
        {   
            SelectCurrentRepresentation();
            _bullet.OnHit += PlayExplosion;
            _bullet.OnHit += StopVisual;
            PlayVisual();
        }

        private void SelectCurrentRepresentation()
        {
            switch (_bullet.Team)
            {
                case TeamType.Player:
                    _currentBody = _bodyBlue;
                    _currentExplosion = _explosionBlue;
                    break;
                
                case TeamType.Enemy:
                    _currentBody = _bodyRed;
                    _currentExplosion = _explosionRed;
                    break;
            }
        }

        private void OnDisable()
        {
            _bullet.OnHit -= PlayExplosion;
            _bullet.OnHit -= StopVisual;
        }

        private void PlayVisual()
        {
           _currentBody.gameObject.SetActive(true);
           _currentBody.Play();
        }

        private void StopVisual(Bullet bullet)
        {
            _currentBody.Stop();
        }

        private void PlayExplosion(Bullet bullet)
        {
            _currentExplosion.gameObject.SetActive(true);
            _currentExplosion.Play();
        }
    }
}