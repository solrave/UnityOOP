
namespace Game.Gameplay
{
    public class PointService
    {
        private SpawnPoint[] _spawnPoints;
        private FirePoint[] _firePoints;

        public PointService(SpawnPoint[] spawnPoints, FirePoint[] firePoints)
        {
            _spawnPoints = spawnPoints;
            _firePoints = firePoints;
        }
        
        public IPoint GetSpawnPoint()
        {
            _spawnPoints.Shuffle();
            return _spawnPoints.GetRandom();
        }
        
        public IPoint GetFirePoint()
        {
            _firePoints.Shuffle();
            return _firePoints.GetRandom();
        }
    }
}