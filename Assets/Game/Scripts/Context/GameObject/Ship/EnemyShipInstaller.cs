using System;
using Game.Scripts.GameObjects.Components;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameObjects.Ship.Context
{
    [Serializable]
    public class EnemyShipInstaller : Installer
    {
        [SerializeField] 
        private float _stoppingDistance;
        
        public override void InstallBindings()
        {
            this.Container.Bind<IFollowComponent>()
                .To<FollowComponent>()
                .AsTransient()
                .WithArguments(_stoppingDistance);
        }
    }
}