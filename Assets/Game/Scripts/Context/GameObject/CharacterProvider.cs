using Game.Scripts.Context.GameObject.Ship;
using Zenject;

namespace Game
{
    public class CharacterProvider
    {
        public Entity Player { get; }
        public CharacterProvider(Entity player) => Player = player;
      
    }
}