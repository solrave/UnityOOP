using System;
using Game.Scripts.ZenjectExtensions;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Systems.Enemy.Spawn.Points
{
    public class FirePointService
    {
        private FirePoint[] _firePoints;

        public FirePointService(FirePoint[] firePoints)
        {
            _firePoints = firePoints;
        }

        public IPoint GetFirePosition()
        {
            _firePoints.Shuffle();
            return _firePoints.GetRandom();
        }
    }
}