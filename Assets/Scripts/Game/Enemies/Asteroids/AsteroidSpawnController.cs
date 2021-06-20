using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawnController : BaseSpawner<List<AsteroidController>>
{
    private const int SMALL_ASTEROID = 0;
    private const int MIDDLE_ASTEROID = 1;
    private const int BIG_ASTEROID = 2;

    private const int ASTEROID_VALUE = 10;
    private SpawnTimeData _spawnTimeData;
    private int _currentAsteroidCount;

    public AsteroidSpawnController(ScoreUI scoreController) : base(scoreController)
    {
        spawnObject = new List<AsteroidController>();
        spawnObject.Add(new AsteroidController(scoreController, "Prefabs/SmallAsteroid", ASTEROID_VALUE, new BaseAsteroidModel("Data/SmallAsteroid")));
        spawnObject.Add(new AsteroidController(scoreController, "Prefabs/MiddleAsteroid", ASTEROID_VALUE, new BaseAsteroidModel("Data/MiddleAsteroid")));
        spawnObject.Add(new AsteroidController(scoreController, "Prefabs/BigAsteroid", ASTEROID_VALUE, new BaseAsteroidModel("Data/BigAsteroid")));
        _spawnTimeData = ResourcesLoader.LoadObject<SpawnTimeData>("Data/AsteroidsSpawnTimer");
        _currentAsteroidCount = 2;

        for (int i = 0; i < _currentAsteroidCount; i++)
        {
            SpawnSpecificAsteroid(BIG_ASTEROID);
        }
        _currentAsteroidCount++;

        for (int i = 0; i < spawnObject.Count; i++)
        {
            if (spawnObject[i] == spawnObject[1])
            {
                spawnObject[i].OnCrack += SpawnSmallAsteroids;
            }
            else if(spawnObject[i] == spawnObject[2])
            {
                spawnObject[i].OnCrack += SpawnMediumAsteroid;
            }
        }
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
        if (!IsAsteroidsActive())
        {
            if (!_spawnTimer.IsOn)
            {
                CalculateTimeToSpawn();
            }
        }
    }

    public override void CalculateTimeToSpawn()
    {
        _spawnTimer.Init(_spawnTimeData.Time);
        _spawnTimer.OnEndCountDown += SpawnAsteroids;
    }

    private void SpawnMediumAsteroid(Transform startPos)
    {
        spawnObject[MIDDLE_ASTEROID].StartMoving(startPos, 45);
        spawnObject[MIDDLE_ASTEROID].StartMoving(startPos, -45);
    }

    private void SpawnSmallAsteroids(Transform startPos)
    {
        spawnObject[SMALL_ASTEROID].StartMoving(startPos, 45);
        spawnObject[SMALL_ASTEROID].StartMoving(startPos, -45);
    }

    private bool IsAsteroidsActive()
    {
        for (int i = 0; i < spawnObject.Count; i++)
        {
            if(spawnObject[i].IsPoolActive())
            {
                return true;
            }
        }
        return false;
    }

    private void SpawnAsteroids()
    {
        for (int i = 0; i < _currentAsteroidCount; i++)
        {
            var asteroidType = Random.Range(SMALL_ASTEROID, BIG_ASTEROID + 1);
            SpawnSpecificAsteroid(asteroidType);
        }
        _currentAsteroidCount++;
        _spawnTimer.OnEndCountDown -= SpawnAsteroids;
    }

    private void SpawnSpecificAsteroid(int asteroidType)
    {
        switch(asteroidType)
        {
            case SMALL_ASTEROID:
                spawnObject[SMALL_ASTEROID].StartMoving();
                break;
            case MIDDLE_ASTEROID:
                spawnObject[MIDDLE_ASTEROID].StartMoving();
                break;
            case BIG_ASTEROID:
                spawnObject[BIG_ASTEROID].StartMoving();
                break;
        }
    }

    public void Dispose()
    {
        _spawnTimer.OnEndCountDown -= SpawnAsteroids;
        for (int i = 0; i < spawnObject.Count; i++)
        {
            if (spawnObject[i] == spawnObject[1])
            {
                spawnObject[i].OnCrack -= SpawnSmallAsteroids;
            }
            else if (spawnObject[i] == spawnObject[2])
            {
                spawnObject[i].OnCrack -= SpawnMediumAsteroid;
            }
        }
    }
}
