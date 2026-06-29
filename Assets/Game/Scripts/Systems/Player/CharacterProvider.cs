using Zenject;

namespace Game.Gameplay
{
    public class CharacterProvider
    {
        public Entity Player { get; }
        public CharacterProvider(Entity player) => Player = player;
      
    }
}