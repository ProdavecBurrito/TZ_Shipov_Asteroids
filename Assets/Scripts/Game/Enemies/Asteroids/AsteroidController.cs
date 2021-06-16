using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class AsteroidController : IUpdate, IDisposable
{
    private const int TOP_SPAWN = 2;
    private const int RIGHT_SPAWN = 2;
    private const int BOT_SPAWN = 0;
    private const int LEFT_SPAWN = 0;
    private const int HALF_VALUE = 2;

    private AsteroidPool _asteroidPool;
    private BaseAsteroidModel _asteroidModel;
    private List<float> _asteroidsSpeed;
    private ScoreUI _scoreUI;

    private int _direction;
    private float _hight;
    private float _width;

    public BaseAsteroidModel AsteroidModel => _asteroidModel;

    public AsteroidController(ScoreUI scoreUI, string asteroidPrefabPath, int count, BaseAsteroidModel asteroidModel)
    {
        _scoreUI = scoreUI;
        _asteroidsSpeed = new List<float>();
        _asteroidModel = asteroidModel;
        _asteroidPool = new AsteroidPool(count, _asteroidModel, asteroidPrefabPath);

        foreach (var item in _asteroidPool.PollObjects)
        {
            item.AsteroidView.OnPlayerHit += AddScore;
        }
    }

    public void UpdateTick()
    {
        _asteroidPool.UpdateTick();
    }

    public void AddScore()
    {
        _scoreUI.AddScore(_asteroidModel.Score);
    }

    private void StartMoving()
    {
        var navigationGO = new GameObject();
        navigationGO.transform.position = CalculateStartPosition();
        navigationGO.transform.rotation.SetLookRotation(CalculateRotation());
        _asteroidPool.TryToAct(navigationGO.transform);
    }

    private Vector3 CalculateRotation()
    {
        var randomRotation = Random.Range(-360, 361);
        var rotationVector = new Vector3(0, 0, randomRotation);
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
                _width = CameraFrustrum.CalculateHight() / HALF_VALUE;
            }
            else
            {
                _width = -CameraFrustrum.CalculateHight() / HALF_VALUE;
            }
        }
        return new Vector2(_width, _hight);
    }

    public void Dispose()
    {
        foreach (var item in _asteroidPool.PollObjects)
        {
            item.AsteroidView.OnPlayerHit -= AddScore;
        }
    }
}
