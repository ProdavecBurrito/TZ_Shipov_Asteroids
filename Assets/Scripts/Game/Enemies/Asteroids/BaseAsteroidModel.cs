using UnityEngine;

public class BaseAsteroidModel
{
    private AsteroidData _asteroidData;
    private float _maxSpeed;
    private float _minSpeed;
    private AudioClip _explosionClip; 

    public int Score { get; set; }
    public AsteroidData AsteroidData => _asteroidData;
    public AudioClip ExplosionClip => _explosionClip;
    public float MaxSpeed => _maxSpeed;
    public float MinSpeed => _minSpeed;

    public BaseAsteroidModel(string dataPath)
    {
        _asteroidData = ResourcesLoader.LoadObject<AsteroidData>(dataPath);
        _explosionClip = ResourcesLoader.LoadObject<AudioClip>("AsteroidExplosion");
        _maxSpeed = _asteroidData.MaxSpeed;
        _minSpeed = _asteroidData.MinSpeed;
        Score = _asteroidData.Score;
    }
}
