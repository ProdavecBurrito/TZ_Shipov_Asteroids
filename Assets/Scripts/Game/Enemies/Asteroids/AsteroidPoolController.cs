using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class AsteroidPoolController : IUpdate, ISpawner, IDisposable
{
    public event Action<Transform> OnCrack = delegate (Transform transform) { };
    public event Action<Asteroid> OnCreation = delegate (Asteroid asteroid) { };

    private const int TOP_SPAWN = 2;
    private const int RIGHT_SPAWN = 2;
    private const int BOT_SPAWN = 0;
    private const int LEFT_SPAWN = 0;
    private const int HALF_VALUE = 2;

    private AsteroidPool _asteroidPool;
    private AsteroidData _asteroidData;
    private ScoreUI _scoreUI;
    private GameObject _navigationGO;
    private AudioSource _audioSource;
    private AudioClip _explosionClip;

    public AsteroidPool AsteroidPool => _asteroidPool;

    public float SpawnHight { get; private set; }

    public float SpawnWidth { get; private set; }

    public AsteroidPoolController(IUIValue scoreUI, string asteroidPrefabPath, int count, AsteroidData asteroidData)
    {
        _asteroidData = asteroidData;
        _scoreUI = (ScoreUI)scoreUI;
        _asteroidPool = new AsteroidPool(count, _asteroidData, asteroidPrefabPath);

        _navigationGO = new GameObject();
        _navigationGO.AddComponent(typeof(AudioSource));

        _audioSource = _navigationGO.GetComponent<AudioSource>();
        _audioSource.volume = 0.5f;
        _explosionClip = _explosionClip = ResourcesLoader.LoadObject<AudioClip>("AsteroidExplosion");
        _audioSource.clip = _explosionClip;


        _asteroidPool.OnCreation += OnAsteroidCreation;

        foreach (var item in _asteroidPool.PollObjects)
        {
            item.OnPlayerHit += AddScore;
            item.OnHit += PlayExplosionSound;
            if (item is CrackingAsteroids asteroidsView)
            {
                asteroidsView.OnCrack += OnAsteroidCrack;
            }
        }
    }

    public void UpdateTick()
    {
        _asteroidPool.UpdateTick();
    }

    public void StartMoving()
    {
        _navigationGO.transform.position = CalculateStartPosition();
        _navigationGO.transform.rotation = CalculateRotation();
        _asteroidPool.TryToAct(_navigationGO.transform);
    }

    public Vector2 CalculateStartPosition()
    {
        var isWidthSpawn = Random.Range(0, 2);
        if (isWidthSpawn != 0)
        {
            SpawnHight = Random.Range(-CameraFrustrum.CalculateHight() / HALF_VALUE, CameraFrustrum.CalculateHight() / HALF_VALUE);
            var spawnWidth = Random.Range(LEFT_SPAWN, RIGHT_SPAWN);
            if (spawnWidth == RIGHT_SPAWN)
            {
                SpawnWidth = CameraFrustrum.CalculateWidth() / HALF_VALUE;
            }
            else
            {
                SpawnWidth = -CameraFrustrum.CalculateWidth() / HALF_VALUE;
            }
        }
        else
        {
            SpawnWidth = Random.Range(-CameraFrustrum.CalculateWidth() / HALF_VALUE, CameraFrustrum.CalculateWidth() / HALF_VALUE);
            var spawnHight = Random.Range(BOT_SPAWN, TOP_SPAWN);

            if (spawnHight == TOP_SPAWN)
            {
                SpawnHight = CameraFrustrum.CalculateHight() / HALF_VALUE;
            }
            else
            {
                SpawnHight = -CameraFrustrum.CalculateHight() / HALF_VALUE;
            }
        }
        return new Vector2(SpawnWidth, SpawnHight);
    }

    public void StartMoving(Transform startPosition, float rotationValue)
    {
        _navigationGO.transform.position = startPosition.position;
        _navigationGO.transform.rotation = CalculateRotation(startPosition, rotationValue);
        _asteroidPool.TryToAct(_navigationGO.transform);
    }

    public bool IsPoolActive()
    {
        return _asteroidPool.CheckAstivity();
    }

    private void PlayExplosionSound()
    {
        _audioSource.Play();
    }

    private void AddScore()
    {
        _scoreUI.ManageValue(_asteroidData.Score);
    }

    private Quaternion CalculateRotation()
    {
        var randomRotation = Random.Range(-360, 361);
        var rotationVector = Quaternion.Euler(0, 0, randomRotation);
        return rotationVector;
    }

    private Quaternion CalculateRotation(Transform transform, float rotationValue)
    {
        var rotationVector = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + rotationValue);
        return rotationVector;
    }

    private void OnAsteroidCreation(Asteroid asteroid)
    {
        if (asteroid is CrackingAsteroids crackingAsteroid)
        {
            crackingAsteroid.OnCrack += OnAsteroidCrack;
        }
    }

    private void OnAsteroidCrack(Transform startPosition)
    {
        OnCrack.Invoke(startPosition);
    }

    public void Dispose()
    {
        _asteroidPool.OnCreation -= OnAsteroidCreation;
        foreach (var item in _asteroidPool.PollObjects)
        {
            item.OnPlayerHit -= AddScore;
            item.OnHit -= PlayExplosionSound;
            if (item is CrackingAsteroids asteroidsView)
            {
                asteroidsView.OnCrack -= OnAsteroidCrack;
            }
        }
    }
}
