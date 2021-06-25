using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class UFOSpawnController : BaseSpawner<UFO>, ISpawner, IUpdate, IDisposable
{
    private const int RIGHT_SPAWN = 1;
    private const int LEFT_SPAWN = 0;
    private const int HALF_VALUE = 2;

    private int _direction;
    private float _fifthPartOfHight;

    public float SpawnHight { get; private set; }
    public float SpawnWidth { get; private set; }

    public UFOSpawnController(ScoreUI scoreUI) : base(scoreUI)
    {
        spawnObject = ResourcesLoader.LoadAndInstantiateObject<UFO>("Prefabs/UFO");
        spawnObject.InjectUI(scoreUI);
        spawnObject.SetActivity(false);
        CalculateTimeToSpawn();
        _fifthPartOfHight = CameraFrustrum.GetFifthPartOfHight();

        _spawnTimer.OnEndCountDown += LaunchUFO;
    }

    public override void UpdateTick()
    {
        base.UpdateTick();
        spawnObject.UpdateTick();
    }

    public Vector2 CalculateStartPosition()
    {
        SpawnHight = Random.Range(-CameraFrustrum.CalculateHight() / HALF_VALUE + _fifthPartOfHight, CameraFrustrum.CalculateHight() / HALF_VALUE - _fifthPartOfHight);
        var spawnWidth = Random.Range(LEFT_SPAWN, RIGHT_SPAWN + 1);
        if (spawnWidth == RIGHT_SPAWN)
        {
            SpawnWidth = CameraFrustrum.CalculateWidth() / HALF_VALUE;
        }
        else
        {
            SpawnWidth = -CameraFrustrum.CalculateWidth() / HALF_VALUE;
        }
        if (SpawnWidth > CameraFrustrum.CalculateHight() / HALF_VALUE)
        {
            _direction = -1;
        }
        else
        {
            _direction = 1;
        }
        return new Vector2(SpawnWidth, SpawnHight);
    }

    public override void TrySpawn()
    {
        if (!spawnObject.IsActive)
        {
            if (!_spawnTimer.IsOn)
            {
                CalculateTimeToSpawn();
            }
        }
    }
    public void LaunchUFO()
    {
        spawnObject.SetActivity(true);
        StartMoving(CalculateStartPosition());
        spawnObject.GetDirection(_direction);
    }

    private void StartMoving(Vector2 startPosition)
    {
        spawnObject.transform.position = startPosition;
    }

    public override void CalculateTimeToSpawn()
    {
        var timeToSpawn = Random.Range(spawnObject.UfoData.MinSpawnTime, spawnObject.UfoData.MaxSpawnTime);
        _spawnTimer.Init(timeToSpawn);
    }

    public void Dispose()
    {
        _spawnTimer.OnEndCountDown -= LaunchUFO;
    }
}
