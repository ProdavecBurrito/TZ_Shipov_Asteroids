using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class BulletPool : IUpdate, IDisposable
{
    private List<Bullet> _bullets = new List<Bullet>();
    private BulletData _bulletData;
    private Timer _timer;
    private bool _isFoundBullet;
    private int _currentBulletIndex;
    private string _prefabPath;

    public BulletPool(int count, string bulletDataPath, string bulletPrefabPath)
    {
        _bulletData = ResourcesLoader.LoadObject<BulletData>(bulletDataPath);
        _prefabPath = bulletPrefabPath;
        _bulletData.MaxDistance = CameraFrustrum.CalculateWidth();
        _timer = new Timer();

        for (int i = 0; i < count; i++)
        {
            AddBullets();
        }

        _currentBulletIndex = 0;
    }

    private void AddBullets()
    {
        var bullet = new Bullet(_bulletData.Speed, _bulletData.MaxDistance, LoadBulletView(), _bulletData.BulletColor);
        _bullets.Add(bullet);
    }

    private void RemoveBullets(int index)
    {
        if (_bullets.Contains(_bullets[index]))
        {
            _bullets.Remove(_bullets[index]);
        }
    }

    public void TryShoot(Transform fireStartPosition)
    {
        if (!_timer.IsOn)
        {
            if (_currentBulletIndex >= _bullets.Count)
            {
                for (int i = 0; i < _bullets.Count; i++)
                {
                    if (!_bullets[i].BulletView.IsActive)
                    {
                        _currentBulletIndex = i;
                        Shoot(fireStartPosition);
                        _isFoundBullet = true;
                        break;
                    }
                }
                if (!_isFoundBullet)
                {
                    AddBullets();
                    Shoot(fireStartPosition);
                }
                _isFoundBullet = false;
            }
            else if (!_bullets[_currentBulletIndex].BulletView.IsActive)
            {
                Shoot(fireStartPosition);
            }
        }
    }

    private void Shoot(Transform fireStartPosition)
    {
        _bullets[_currentBulletIndex].Fire(fireStartPosition);
        var time = Random.Range(_bulletData.MinDelay, _bulletData.MaxDelay);
        _timer.Init(time);
        _currentBulletIndex++;
    }

    private BaseBulletView LoadBulletView()
    {
        return ResourcesLoader.LoadAndInstantiateObject<BaseBulletView>(_prefabPath);
    }

    public void Dispose()
    {
        _bullets.Clear();
    }

    public void UpdateTick()
    {
        for (int i = 0; i < _bullets.Count; i++)
        {
            _bullets[i].Fly();
        }
        _timer.CountTime();
    }
}
