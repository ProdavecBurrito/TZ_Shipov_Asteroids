using System;
using Random = UnityEngine.Random;

public class UFOSpawnController : IUpdate , IDisposable
{
    private const int BULLET_PULL_COUNT = 5;

    private Timer _timer;
    private UFOController _ufoCountroller;
    private BulletPool _bulletPool;

    public UFOSpawnController(BattleUnitView battleUnitView, ShipView target, ScoreUI scoreController)
    {
        _bulletPool = new BulletPool(BULLET_PULL_COUNT, "Data/UFOBullet", "Prefabs/UFOBullet");
        _ufoCountroller = new UFOController(battleUnitView, target, _bulletPool, scoreController);
        _timer = new Timer();
        CalculateTime();
    }

    public void UpdateTick()
    {
        TrySpawnUFO();
        _ufoCountroller.UpdateTick();
        _bulletPool.UpdateTick();
        _timer.CountTime();
    }

    private void TrySpawnUFO()
    {
        if (!_ufoCountroller.UfoView.IsActive)
        {
            if (!_timer.IsOn)
            {
                CalculateTime();
            }
        }
    }

    private void CalculateTime()
    {
        var timeToSpawn = Random.Range(_ufoCountroller.UfoModel.UfoData.MinSpawnTime, _ufoCountroller.UfoModel.UfoData.MaxSpawnTime);
        _timer.Init(timeToSpawn);
        _timer.OnEndCountDown += _ufoCountroller.LaunchUFO;
    }

    public void Dispose()
    {
        _timer.OnEndCountDown -= _ufoCountroller.LaunchUFO;
    }
}
