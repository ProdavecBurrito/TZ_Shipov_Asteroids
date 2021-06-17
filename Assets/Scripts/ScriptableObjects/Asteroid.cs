using UnityEngine;

public class Asteroid
{
    private BaseEnemyView _asteroidView;

    private float _speed;

    public BaseEnemyView AsteroidView => _asteroidView;

    public Asteroid(BaseEnemyView asteroid)
    {
        _asteroidView = asteroid;
    }

    public void Fly()
    {
        if (_asteroidView.IsActive)
        {
            _asteroidView.transform.Translate(-Vector2.up * _speed * Time.deltaTime);
        }
        else
        {
            ReturnToPool();
        }
    }

    public void LaunchAsteroid(Transform startPosition)
    {
        _asteroidView.UnitTransform.position = startPosition.position;
        _asteroidView.UnitTransform.rotation = startPosition.rotation;
        _asteroidView.SetActivity(true);
    }


    public void AssignSpeed(float value)
    {
        _speed = value;
    }

    public void ReturnToPool()
    {
        _asteroidView.SetActivity(false);
    }
}
