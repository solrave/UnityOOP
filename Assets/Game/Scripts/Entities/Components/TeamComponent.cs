using System;
using UnityEngine;

namespace Game.Gameplay
{
    [Serializable]
    public class TeamComponent
    {
        public event Action<TeamType> OnTeamChanged;

        [field: SerializeField] 
        public TeamType _team;

        public TeamType Team
        {
            get => _team;
            set
            {
                _team = value;
                OnTeamChanged?.Invoke(Team);
            }
        }
    }
}