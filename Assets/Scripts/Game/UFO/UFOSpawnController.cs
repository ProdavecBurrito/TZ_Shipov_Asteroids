using System;
using Random = UnityEngine.Random;

public class UFOSpawnController : IDisposable
{
    private Timer _timer;
    private UFOCountroller _ufoCountroller;

    public UFOSpawnController(BattleUnitView battleUnitView)
    {
        _ufoCountroller = new UFOCountroller(battleUnitView);
        _timer = new Timer();
        CalculateTime();
        UpdatingController.SubscribeToTUpdate(TrySpawnUFO);
    }

    public void TrySpawnUFO()
    {
        if (!_ufoCountroller.UfoView.IsActive)
        {
            if (!_timer.IsOn)
            {
                CalculateTime();
            }
        }
    }

    public void CalculateTime()
    {
        var timeToSpawn = Random.Range(_ufoCountroller.UfoModel.UfoData.MinSpawnTime, _ufoCountroller.UfoModel.UfoData.MaxSpawnTime);
        _timer.Init(timeToSpawn);
        _timer.EndCountDown += _ufoCountroller.Launch;
    }

    public void Dispose()
    {
        UpdatingController.UnsubscribeFromUpdate(TrySpawnUFO);
    }
}
