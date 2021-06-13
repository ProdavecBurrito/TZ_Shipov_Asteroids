using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class UFOCountroller : IDisposable
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

    public UFOCountroller(BattleUnitView battleUnitView)
    {
        _ufoView = (UFOView)battleUnitView;
        _ufoModel = new UFOModel();
        _bulletPool = new BulletPool(BULLET_PULL_COUNT, "Data/UFOBullet");
        _fifthPartOfHight = CameraFrustrum.GetFifthPartOfHight();
    }

    public void UpdateTick()
    {
        FolowTarget();
        Move();
        TryToShoot();
    }

    public void FolowTarget()
    {
        _ufoView.FireStartPosition.position = _ufoView.ShipTransform.position - _ufoView.Target.position;
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
        _hight = Random.Range(-CameraFrustrum.CalculateHight() / 2, CameraFrustrum.CalculateHight() / 2);
        var num = Random.Range(0, 2);
        if (num == 1)
        {
            Debug.Log("1");
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
        Debug.Log(_width);
        Debug.Log(_hight);
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
        UpdatingController.SubscribeToTUpdate(UpdateTick);
    }

    public void Die()
    {
        UpdatingController.UnsubscribeFromUpdate(UpdateTick);
        _ufoView.SetActivity(false);
    }

    public void Dispose()
    {
        UpdatingController.UnsubscribeFromUpdate(UpdateTick);
    }
}
