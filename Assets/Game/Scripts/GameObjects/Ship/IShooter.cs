using System;
using UnityEngine;


namespace Game
{
    public interface IShooter
    {
        public event Action<Transform, TeamType> OnFire;
    }
}