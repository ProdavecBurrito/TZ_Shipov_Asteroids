using System;
using Random = UnityEngine.Random;

public class UFOSpawnController : BaseSpawner<UFOController>, IDisposable, IUpdate
{
    private const int BULLET_PULL_COUNT = 5;
    private BasePool<Bullet> _bulletPool;

    public UFOSpawnController(ScoreUI scoreController) : base(scoreController)
    {
        _bulletPool = new BulletPool(BULLET_PULL_COUNT, "Data/UFOBullet", "Prefabs/UFOBullet");
        spawnObject = new UFOController(_bulletPool, scoreController);
        CalculateTimeToSpawn();
    }

    public override void UpdateTick()
    {
        base.UpdateTick();
        spawnObject.UpdateTick();
        _bulletPool.UpdateTick();
    }

    public override void TrySpawn()
    {
        if (!spawnObject.UfoView.IsActive)
        {
            if (!_spawnTimer.IsOn)
            {
                CalculateTimeToSpawn();
            }
        }
    }

    public override void CalculateTimeToSpawn()
    {
        var timeToSpawn = Random.Range(spawnObject.UfoModel.UfoData.MinSpawnTime, spawnObject.UfoModel.UfoData.MaxSpawnTime);
        _spawnTimer.Init(timeToSpawn);
        _spawnTimer.OnEndCountDown += spawnObject.LaunchUFO;
    }

    public void Dispose()
    {
        _spawnTimer.OnEndCountDown -= spawnObject.LaunchUFO;
    }
}
