using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class UFOController : IDisposable
{
    private const int BULLET_PULL_COUNT = 5;

    private UFOView _ufoView;
    private UFOModel _ufoModel;
    private BulletPool _bulletPool;


    private int _direction;
    private float _hight;
    private float _width;
    private float _fifthPartOfHight;

    public UFOModel UfoModel => _ufoModel;

    public UFOView UfoView => _ufoView;

    public UFOController(BattleUnitView battleUnitView, ShipView target)
    {
        _ufoView = (UFOView)battleUnitView;
        _ufoView.GetTarget(target);
        _ufoModel = new UFOModel();
        _bulletPool = new BulletPool(BULLET_PULL_COUNT, "Data/UFOBullet");
        _fifthPartOfHight = CameraFrustrum.GetFifthPartOfHight();
    }

    public void UpdateTick()
    {
        if (_ufoView.IsActive)
        {
            FolowTarget();
            Move();
            TryToShoot();
            _bulletPool.UpdateTick();
        }
    }

    public void FolowTarget()
    {
        var direction = _ufoView.Target.position - _ufoView.ShipTransform.position;
        var position = -(_ufoView.ShipTransform.position - _ufoView.Target.position).normalized;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90.0f;
        _ufoView.FireStartPosition.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        _ufoView.FireStartPosition.localPosition = position;
    }

    public void StartMoving(Vector2 startPosition)
    {
        _ufoView.ShipTransform.position = startPosition;
    }

    public void Move()
    {
        _ufoView.ShipTransform.Translate(Vector2.right * _direction * _ufoModel.Speed * Time.deltaTime);
    }

    public Vector2 CalculateStartPosition()
    {
        _hight = Random.Range(-CameraFrustrum.CalculateHight() / 2 + _fifthPartOfHight, CameraFrustrum.CalculateHight() / 2 - _fifthPartOfHight);
        var spawnWidth = Random.Range(0, 2);
        if (spawnWidth == 1)
        {
            _width = CameraFrustrum.CalculateWidth() / 2;
        }
        else
        {
            _width = -CameraFrustrum.CalculateWidth() / 2;
        }
        if (_width > CameraFrustrum.CalculateHight() / 2)
        {
            _direction = -1;
        }
        else
        {
            _direction = 1;
        }
        return new Vector2(_width, _hight);
    }

    public void TryToShoot()
    {
        _bulletPool.TryShoot(_ufoView.FireStartPosition);
    }

    public void Launch()
    {
        _ufoView.SetActivity(true);
        StartMoving(CalculateStartPosition());
    }

    public void Die()
    {
        _ufoView.SetActivity(false);
    }

    public void Dispose()
    {
    }
}
