using UnityEngine;
using System;

public class Bullet
{
    private float _currentPosition;
    private BaseBulletView _bulletView;

    private float _speed;
    private float _maxRange;

    public BaseBulletView BulletView => _bulletView;

    public Bullet(float speed, float maxRange, BaseBulletView bulletView, Color bulletColor)
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
            if (_currentPosition < _maxRange)
            {
                _currentPosition += _speed * Time.deltaTime;
                _bulletView.transform.Translate(-Vector2.up * _speed * Time.deltaTime);
            }
            else
            {
                ReturnToPool();
            }
        }
    }

    public void Fire(Transform fireStartPosition)
    {
        _bulletView.transform.position = fireStartPosition.position;
        _bulletView.transform.rotation = fireStartPosition.rotation;
        _currentPosition = 0;
        _bulletView.ChangeActiveState(true);
    }

    public void ReturnToPool()
    {
        _bulletView.ChangeActiveState(false);
    }
}
