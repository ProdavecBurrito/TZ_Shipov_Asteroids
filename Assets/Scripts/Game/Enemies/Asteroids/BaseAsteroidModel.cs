public class BaseAsteroidModel
{
    private AsteroidData _asteroidData;
    private float _maxSpeed;
    private float _minSpeed;

    public int Score { get; set; }
    public AsteroidData AsteroidData => _asteroidData;

    public float MaxSpeed => _maxSpeed;
    public float MinSpeed => _minSpeed;

    public BaseAsteroidModel(string dataPath)
    {
        _asteroidData = ResourcesLoader.LoadObject<AsteroidData>(dataPath);
        _maxSpeed = _asteroidData.MaxSpeed;
        _minSpeed = _asteroidData.MinSpeed;
        Score = _asteroidData.Score;
    }
}
