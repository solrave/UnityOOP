using Game.Scripts.GameObjects.Ship;
using Zenject;

namespace Game.Scripts.Context.GameObject.Ship.Enemy
{
    public class EnemyEntity : GameEntity
    {
        private EnemyAI _ai;

        [Inject]
        public void Construct(EnemyAI ai)
        {
            _ai = ai;
        }
        
        public sealed class Factory : PlaceholderFactory<EnemyAI,EnemyEntity>
        {
        }
    }
}