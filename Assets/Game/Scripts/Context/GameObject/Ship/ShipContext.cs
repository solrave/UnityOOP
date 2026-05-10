using Zenject;

namespace Game.Scripts.GameObjects.Ship
{
    public class ShipContext : GameObjectContext
    {
        public TeamType Team { get; private set; }

        [Inject]
        public void Construct(TeamType team)
        {
            Team = team;
        }
        
    }
}