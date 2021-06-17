using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class AsteroidController : IUpdate, IDisposable
{
    public event Action<Transform> OnCrack = delegate (Transform transform) { };
    public event Action<Asteroid> OnCreation = delegate (Asteroid asteroid) { };

    private const int TOP_SPAWN = 2;
    private const int RIGHT_SPAWN = 2;
    private const int BOT_SPAWN = 0;
    private const int LEFT_SPAWN = 0;
    private const int HALF_VALUE = 2;

    private AsteroidPool _asteroidPool;
    private BaseAsteroidModel _asteroidModel;
    private ScoreUI _scoreUI;
    private GameObject navigationGO;
    private AudioSource _audioSource;

    private float _hight;
    private float _width;

    public AsteroidPool AsteroidPool => _asteroidPool;

    public AsteroidController(ScoreUI scoreUI, string asteroidPrefabPath, int count, BaseAsteroidModel asteroidModel)
    {
        _scoreUI = scoreUI;
        _asteroidModel = asteroidModel;
        _asteroidPool = new AsteroidPool(count, _asteroidModel, asteroidPrefabPath);
        navigationGO = new GameObject();
        navigationGO.AddComponent(typeof(AudioSource));
        _audioSource = navigationGO.GetComponent<AudioSource>();
        _audioSource.volume = 0.5f;
        _audioSource.clip = _asteroidModel.ExplosionClip;

        _asteroidPool.OnCreation += OnAsteroidCreation;

        foreach (var item in _asteroidPool.PollObjects)
        {
            item.AsteroidView.OnPlayerHit += AddScore;
            item.AsteroidView.OnHit += PlayExplosionSound;
            if (item.AsteroidView is CrackingAsteroidsView asteroidsView)
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
        navigationGO.transform.position = CalculateStartPosition();
        navigationGO.transform.rotation = CalculateRotation();
        _asteroidPool.TryToAct(navigationGO.transform);
    }

    public void StartMoving(Transform startPosition, float rotationValue)
    {
        navigationGO.transform.position = startPosition.position;
        navigationGO.transform.rotation = CalculateRotation(startPosition, rotationValue);
        _asteroidPool.TryToAct(navigationGO.transform);
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
        _scoreUI.AddScore(_asteroidModel.Score);
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

    private Vector2 CalculateStartPosition()
    {
        var isWidthSpawn = Random.Range(0, 2);
        if (isWidthSpawn != 0)
        {
            _hight = Random.Range(-CameraFrustrum.CalculateHight() / HALF_VALUE, CameraFrustrum.CalculateHight() / HALF_VALUE);
            var spawnWidth = Random.Range(LEFT_SPAWN, RIGHT_SPAWN);
            if (spawnWidth == RIGHT_SPAWN)
            {
                _width = CameraFrustrum.CalculateWidth() / HALF_VALUE;
            }
            else
            {
                _width = -CameraFrustrum.CalculateWidth() / HALF_VALUE;
            }
        }
        else
        {
            _width = Random.Range(-CameraFrustrum.CalculateWidth() / HALF_VALUE, CameraFrustrum.CalculateWidth() / HALF_VALUE);
            var spawnHight = Random.Range(BOT_SPAWN, TOP_SPAWN);

            if (spawnHight == TOP_SPAWN)
            {
                _hight = CameraFrustrum.CalculateHight() / HALF_VALUE;
            }
            else
            {
                _hight = -CameraFrustrum.CalculateHight() / HALF_VALUE;
            }
        }
        return new Vector2(_width, _hight);
    }

    private void OnAsteroidCreation(Asteroid asteroid)
    {
        if (asteroid.AsteroidView is CrackingAsteroidsView crackingAsteroid)
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
            item.AsteroidView.OnPlayerHit -= AddScore;
            if (item.AsteroidView is CrackingAsteroidsView asteroidsView)
            {
                asteroidsView.OnCrack -= OnAsteroidCrack;
            }
        }
    }
}
