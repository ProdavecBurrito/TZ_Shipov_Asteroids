using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawnController : BaseSpawner<List<AsteroidController>>
{
    private const int ASTEROID_VALUE = 5;
    private SpawnTimeData _spawnTimeData;

    private float _spawnTime;

    public AsteroidSpawnController(ScoreUI scoreController) : base(scoreController)
    {
        spawnObject = new List<AsteroidController>();
        spawnObject.Add(new AsteroidController(scoreController, "Prefabs/SmallAsteroid", ASTEROID_VALUE, new BaseAsteroidModel("Data/SmallAsteroid")));
        spawnObject.Add(new AsteroidController(scoreController, "Prefabs/MiddleAsteroid", ASTEROID_VALUE, new BaseAsteroidModel("Data/MiddleAsteroid")));
        spawnObject.Add(new AsteroidController(scoreController, "Prefabs/BigAsteroid", ASTEROID_VALUE, new BaseAsteroidModel("Data/BigAsteroid")));
        _spawnTimeData = ResourcesLoader.LoadObject<SpawnTimeData>("Data/AsteroidsSpawnTimer");

        CalculateTimeToSpawn();
    }

    public override void UpdateTick()
    {
        base.UpdateTick();
        for (int i = 0; i < spawnObject.Count; i++)
        {
            spawnObject[i].UpdateTick();
        }
    }

    public override void TrySpawn()
    {
        //if (!spawnObject.UfoView.IsActive)
        //{
        //    if (!_spawnTimer.IsOn)
        //    {
        //        CalculateTimeToSpawn();
        //    }
        //}
    }

    public override void CalculateTimeToSpawn()
    {
        //var timeToSpawn = Random.Range(spawnObject.UfoModel.UfoData.MinSpawnTime, spawnObject.UfoModel.UfoData.MaxSpawnTime);
        //_spawnTimer.Init(timeToSpawn);
        //_spawnTimer.OnEndCountDown += spawnObject.;
    }

    public void Dispose()
    {
        //_spawnTimer.OnEndCountDown -= spawnObject.LaunchUFO;
    }
}
