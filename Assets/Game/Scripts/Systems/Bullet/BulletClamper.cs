using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Systems.Pool;
using Modules.Utils;
using UnityEngine;

namespace Game
{
    public class BulletClamper
    {
        private readonly LevelBounds _levelBounds;

        public BulletClamper(LevelBounds levelBounds)
        {
            _levelBounds = levelBounds;
        }
        
        private void ClampPosition(Bullet bullet)
        {
            if (!_levelBounds.InBounds(bullet.Position))
            {
                bullet.Dispose();
            }
        }
    }
}