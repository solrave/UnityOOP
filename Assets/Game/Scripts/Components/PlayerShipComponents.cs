using System;
using Game.Scripts.Components.Core;
using Game.Scripts.Context.GameObject.Ship;
using Game.Scripts.GameObjects.Ship;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Components
{
    public class PlayerShipComponents : ShipComponents
    {
        public PlayerShipComponents(IHealthComponent healthComponent, 
            IMoveComponent moveComponent, IFireComponent fireComponent)
            : base(healthComponent, moveComponent, fireComponent)
        {
        }
    }
}