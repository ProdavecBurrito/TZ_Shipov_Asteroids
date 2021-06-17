using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class UFOController : IUpdate, IDisposable
{
    private const int RIGHT_SPAWN = 1;
    private const int LEFT_SPAWN = 0;
    private const int HALF_VALUE = 2;

    private UFOView _ufoView;
    private UFOModel _ufoModel;
    private BulletPool _bulletPool;
    private AudioSource _audioSource;
    private GameObject navigationGO;
    private ScoreUI _scoreUI;

    private int _direction;
    private float _hight;
    private float _width;
    private float _fifthPartOfHight;

    public UFOModel UfoModel => _ufoModel;

    public UFOView UfoView => _ufoView;

    public UFOController(BasePool<Bullet> bulletPool, ScoreUI scoreUI)
    {
        _ufoView = ResourcesLoader.LoadAndInstantiateObject<UFOView>("Prefabs/UFO");
        _ufoModel = new UFOModel();
        _bulletPool = (BulletPool)bulletPool;
        _fifthPartOfHight = CameraFrustrum.GetFifthPartOfHight();
        _scoreUI = scoreUI;

        navigationGO = new GameObject();
        navigationGO.AddComponent(typeof(AudioSource));
        _audioSource = navigationGO.GetComponent<AudioSource>();
        _audioSource.volume = 0.5f;
        _audioSource.clip = _ufoModel.ExplosionClip;

        _bulletPool.OnFire += PlayFireSound;
        _ufoView.OnHit += PlayExplosionSound;
        _ufoView.OnPlayerHit += AddScore;
    }

    public void UpdateTick()
    {
        ExecuteUpdate();
    }

    public void AddScore()
    {
        _scoreUI.AddScore(_ufoModel.Score);
    }

    public void LaunchUFO()
    {
        Debug.Log("A");
        _ufoView.SetActivity(true);
        StartMoving(CalculateStartPosition());
    }

    private void ExecuteUpdate()
    {
        if (_ufoView.IsActive)
        {
            FolowTarget();
            Move();
            TryToShoot();
        }
    }

    private void FolowTarget()
    {
        var direction = _ufoView.Target.position - _ufoView.UnitTransform.position;
        var position = -(_ufoView.UnitTransform.position - _ufoView.Target.position).normalized;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90.0f;
        _ufoView.FireStartPosition.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        _ufoView.FireStartPosition.localPosition = position;
    }

    private void StartMoving(Vector2 startPosition)
    {
        _ufoView.UnitTransform.position = startPosition;
    }

    private void PlayFireSound()
    {
        _audioSource.clip = _ufoModel.FireClip;
        _audioSource.Play();
    }

    private void PlayExplosionSound()
    {
        _audioSource.clip = _ufoModel.ExplosionClip;
        _audioSource.Play();
    }

    private void Move()
    {
        _ufoView.UnitTransform.Translate(Vector2.right * _direction * _ufoModel.Speed * Time.deltaTime);
    }

    private Vector2 CalculateStartPosition()
    {
        _hight = Random.Range(-CameraFrustrum.CalculateHight() / HALF_VALUE + _fifthPartOfHight, CameraFrustrum.CalculateHight() / HALF_VALUE - _fifthPartOfHight);
        var spawnWidth = Random.Range(LEFT_SPAWN, RIGHT_SPAWN + 1);
        if (spawnWidth == RIGHT_SPAWN)
        {
            _width = CameraFrustrum.CalculateWidth() / HALF_VALUE;
        }
        else
        {
            _width = -CameraFrustrum.CalculateWidth() / HALF_VALUE;
        }
        if (_width > CameraFrustrum.CalculateHight() / HALF_VALUE)
        {
            _direction = -1;
        }
        else
        {
            _direction = 1;
        }
        return new Vector2(_width, _hight);
    }

    private void TryToShoot()
    {
        _bulletPool.TryToAct(_ufoView.FireStartPosition);
    }

    public void Dispose()
    {
        _bulletPool.OnFire += PlayFireSound;
        _ufoView.OnHit += PlayExplosionSound;
        _ufoView.OnPlayerHit += AddScore;
    }
}
