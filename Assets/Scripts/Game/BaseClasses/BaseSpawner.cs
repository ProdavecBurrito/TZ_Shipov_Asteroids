public abstract class BaseSpawner<T> : IUpdate
{
    protected Timer _spawnTimer;
    protected T spawnObject;

    public BaseSpawner(ScoreUI scoreController)
    {
        _spawnTimer = new Timer();
    }

    public virtual void UpdateTick()
    {
        TrySpawn();
        _spawnTimer.CountTime();
    }

    public abstract void TrySpawn();

    public abstract void CalculateTimeToSpawn();
}
