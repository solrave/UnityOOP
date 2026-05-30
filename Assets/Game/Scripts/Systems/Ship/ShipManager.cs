using Zenject;

namespace DefaultNamespace;

public class ShipManager : ITickable
{
    private float _lastSpawnedTime = 0f;
    private readonly ShipManagerSettings _settings;
    private readonly ShipSpawner _shipSpawner;
        
    protected ShipManager(ShipManagerSettings settings, ShipSpawner shipSpawner)
    {
        _settings = settings;
        _shipSpawner = shipSpawner;
    }

    public void Tick()
    {
        if (TimeToSpawn())
            _shipSpawner.Spawn();
    }

    private bool TimeToSpawn()
    {
        _lastSpawnedTime += Time.deltaTime;
        if (_lastSpawnedTime >= _settings.spawnCooldown)
        {
            ResetTimer();
            return true;
        }

        return false;
    }

    private void ResetTimer()
    {
        _lastSpawnedTime = 0f;
    }
}