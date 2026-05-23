using System;
using Game.Scripts.Systems.Enemy.Spawn.Points;
using UnityEngine;
using Zenject;

namespace Game.Scripts
{
    public class GameBootstrapper : MonoBehaviour
    {
        [Inject] private FirePointService _firePointService;

        private void OnEnable()
        {
           Debug.Log($"SPAWNER IN: {_firePointService is not null}");    
        }

        private void OnDisable()
        {
        }
    }
}