using System;
using System.Collections.Generic;
using Game.Components;
using Game.Scripts.GameObjects.Ship;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Game.Scripts
{
    public class ShipInstaller : MonoInstaller
    {
        [SerializeField]
        private ShipContext _shipContext;
        
        [Header("Settings")]
        [SerializeField] 
        private float _fireCooldown;
        
        [SerializeField] 
        private Transform  _firePoint;
        
        [SerializeField]
        private int _maxHealth;

        [SerializeField]
        private float _moveSpeed;
        
        [SerializeField] 
        private Rigidbody _rigidbody;
        
        [SerializeField]
        private Ship _shipPrefab;
        
        public override void InstallBindings()
        {
            this.Container.Bind<IFireComponent>().To<FireComponent>().AsTransient()
                .WithArguments(_firePoint, _fireCooldown);
            
            this.Container.Bind<IMoveComponent>().To<MoveComponent>().AsTransient()
                .WithArguments(_rigidbody, _moveSpeed);
            
            this.Container.Bind<IHealthComponent>().To<HealthComponent>().AsTransient()
                .WithArguments(_maxHealth);

            this.Container.BindInstance(TeamType.Enemy);
        }
    }
}