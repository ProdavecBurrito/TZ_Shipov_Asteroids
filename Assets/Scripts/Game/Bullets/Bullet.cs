using UnityEngine;
using System;

public class Bullet : IDisposable
{
    private float _startPosition;
    private BulletView _bulletView;

    private float _speed;
    private float _maxRange;

    public BulletView BulletView => _bulletView;

    public Bullet(float speed, float maxRange, BulletView bulletView, Color bulletColor)
    {
        _speed = speed;
        _maxRange = maxRange;
        _bulletView = bulletView;
        _bulletView.SetColor(bulletColor);
    }

    public void Fly()
    {
        if (_bulletView.IsActive)
        {
            if (_startPosition < _maxRange)
            {
                _startPosition += _speed * Time.deltaTime;
                _bulletView.transform.Translate(-Vector2.up * _speed * Time.deltaTime);
            }
            else
            {
                ReturnToPool();
            }
        }
    }

    public void Fire(Transform fireStartTransform)
    {
        _bulletView.transform.position = fireStartTransform.position;
        _bulletView.transform.rotation = fireStartTransform.rotation;
        _startPosition = 0;
        _bulletView.ChangeActiveState(true);
    }

    public void ReturnToPool()
    {
        _bulletView.ChangeActiveState(false);
    }

    public void Dispose()
    {
    }
}
