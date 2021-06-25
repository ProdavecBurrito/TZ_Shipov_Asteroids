using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidPool : BasePool<Asteroid>
{
    public event Action<Asteroid> OnCreation = delegate (Asteroid asteroid) { };

    private AsteroidData _asteroidData;
    private bool _isFoundAsteroid;
    private string _prefabPath;

    public AsteroidPool(int count, AsteroidData asteroidData, string astroidPrefabPath) : base(count)
    {
        _asteroidData = asteroidData;
        _prefabPath = astroidPrefabPath;

        for (int i = 0; i < count; i++)
        {
            CreateNewAsteroid();
        }
    }

    public bool CheckAstivity()
    {
        for (int i = 0; i < _poolObjects.Count; i++)
        {
            if (_poolObjects[i].IsActive)
            {
                return true;
            }
        }
        return false;
    }

    public override void UpdateTick()
    {
        for (int i = 0; i < _poolObjects.Count; i++)
        {
            _poolObjects[i].Fly();
        }
    }

    public override void TryToAct(Transform startPosition)
    {
        if (_currentObjectIndex >= _poolObjects.Count)
        {
            for (int i = 0; i < _poolObjects.Count; i++)
            {
                if (!_poolObjects[i].IsActive)
                {
                    _currentObjectIndex = i;
                    Launch(startPosition);
                    _isFoundAsteroid = true;
                    break;
                }
            }
            if (!_isFoundAsteroid)
            {
                CreateNewAsteroid();
                Launch(startPosition);
            }
        _isFoundAsteroid = false;
        }
        else if (!_poolObjects[_currentObjectIndex].IsActive)
        {
            Launch(startPosition);
        }
    }

    private void Launch(Transform startPosition)
    {
        _poolObjects[_currentObjectIndex].Launch(startPosition);
        var speed = Random.Range(_asteroidData.MinSpeed, _asteroidData.MaxSpeed);
        _poolObjects[_currentObjectIndex].AssignSpeed(speed);
        _currentObjectIndex++;
    }

    private Asteroid LoadNewAsteroid()
    {
        return ResourcesLoader.LoadAndInstantiateObject<Asteroid>(_prefabPath);
    }

    private Asteroid CreateNewAsteroid()
    {
        var asteroid = LoadNewAsteroid();
        AddToPool(asteroid);
        OnCreation.Invoke(asteroid);
        return asteroid;
    }
}
