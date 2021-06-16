using UnityEngine;

public class AsteroidPool : BasePool<Asteroid>
{
    private BaseAsteroidModel _asteroidModel;
    private bool _isFoundAsteroid;
    private string _prefabPath;

    public AsteroidPool(int count, BaseAsteroidModel asteroidModel, string astroidPrefabPath) : base(count)
    {
        _asteroidModel = asteroidModel;
        _prefabPath = astroidPrefabPath;

        for (int i = 0; i < count; i++)
        {
            CreateNewAsteroid();
        }
    }

    public override void TryToAct(Transform startPosition)
    {
        if (_currentObjectIndex >= _poolObjects.Count)
        {
            for (int i = 0; i < _poolObjects.Count; i++)
            {
                if (!_poolObjects[i].AsteroidView.IsActive)
                {
                    _currentObjectIndex = i;
                    Launch(startPosition);
                    _isFoundAsteroid = true;
                    break;
                }
            }
            if (!_isFoundAsteroid)
            {
                Launch(startPosition);
            }
        _isFoundAsteroid = false;
        }
        else if (!_poolObjects[_currentObjectIndex].AsteroidView.IsActive)
        {
            Launch(startPosition);
        }
    }

    private void Launch(Transform startPosition)
    {
        _poolObjects[_currentObjectIndex].LaunchAsteroid(startPosition);
        var speed = Random.Range(_asteroidModel.MinSpeed, _asteroidModel.MaxSpeed);
        _currentObjectIndex++;
    }

    private BaseEnemyView LoadAsteroidView()
    {
        return ResourcesLoader.LoadAndInstantiateObject<BaseEnemyView>(_prefabPath);
    }

    private Asteroid CreateNewAsteroid()
    {
        var asteroid = new Asteroid(LoadAsteroidView());
        AddToPool(asteroid);
        return asteroid;
    }

    public override void UpdateTick()
    {
        for (int i = 0; i < _poolObjects.Count; i++)
        {
            _poolObjects[i].Fly();
        }
    }
}
