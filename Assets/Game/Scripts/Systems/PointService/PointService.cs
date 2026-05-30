
namespace Game.Gameplay
{
    public class PointService
    {
        private Point[] _spawnPoints;
        private Point[] _firePoints;

        public PointService(Point[] spawnPoints, Point[] firePoints)
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