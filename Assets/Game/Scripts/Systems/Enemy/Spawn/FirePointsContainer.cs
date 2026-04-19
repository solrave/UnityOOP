using System;
using Modules.Utils;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class FirePointsContainer
    {
        [SerializeField]
        private Transform[] _firePoints;

        public Vector2 GetFirePosition()
        {
            _firePoints.Shuffle();
            Transform firePosition = _firePoints.GetRandom();
            return firePosition.position;
        }
    }
}