using System.Collections.Generic;
using UnityEngine;

public class BulletPool
{
    private List<Bullet> _bullets = new List<Bullet>();
    private BulletData _bulletData;
    private Timer _timer;
    private bool _isFoundBullet;
    private int _currentBulletIndex;

    public BulletPool(int count, string bulletDataPath)
    {
        _bulletData = ResourcesLoader.LoadObject<BulletData>(bulletDataPath);
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

    private BulletView LoadBulletView()
    {
        return ResourcesLoader.LoadAndInstantiateObject<BulletView>("Prefabs/Bullet");
    }
}
