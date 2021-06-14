using System;
using Random = UnityEngine.Random;

public class UFOSpawnController : IUpdate , IDisposable
{
    private Timer _timer;
    private UFOController _ufoCountroller;

    public UFOSpawnController(BattleUnitView battleUnitView, ShipView target)
    {
        _ufoCountroller = new UFOController(battleUnitView, target);
        _timer = new Timer();
        CalculateTime();
    }

    public void UpdateTick()
    {
        TrySpawnUFO();
        _ufoCountroller.UpdateTick();
        _timer.CountTime();
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
        _timer.EndCountDown -= _ufoCountroller.Launch;
    }
}
