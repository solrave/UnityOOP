using System;
using Game.Scripts.ZenjectExtensions;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class FirePointService
    {
        [SerializeField]
        private FirePoint[] _firePoints;

        public IPoint GetFirePosition()
        {
            _firePoints.Shuffle();
            return _firePoints.GetRandom();
        }
    }
}