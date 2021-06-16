using UnityEngine;
using Random = UnityEngine.Random;

public class BulletPool : BasePool<Bullet>, IUpdate
{
    private BulletData _bulletData;
    private bool _isFoundBullet;
    private int _currentBulletIndex;
    protected Timer _timer;
    private string _prefabPath;

    public BulletPool(int count, string bulletDataPath, string bulletPrefabPath) : base(count)
    {
        _bulletData = ResourcesLoader.LoadObject<BulletData>(bulletDataPath);
        _prefabPath = bulletPrefabPath;
        _bulletData.MaxDistance = CameraFrustrum.CalculateWidth();
        _timer = new Timer();

        for (int i = 0; i < count; i++)
        {
            CreateNewBullet();
        }
    }

    public override void TryToAct(Transform fireStartPosition)
    {
        if (!_timer.IsOn)
        {
            if (_currentBulletIndex >= _poolObjects.Count)
            {
                for (int i = 0; i < _poolObjects.Count; i++)
                {
                    if (!_poolObjects[i].BulletView.IsActive)
                    {
                        _currentBulletIndex = i;
                        Shoot(fireStartPosition);
                        _isFoundBullet = true;
                        break;
                    }
                }
                if (!_isFoundBullet)
                {
                    Shoot(fireStartPosition);
                }
                _isFoundBullet = false;
            }
            else if (!_poolObjects[_currentBulletIndex].BulletView.IsActive)
            {
                Shoot(fireStartPosition);
            }
        }
    }

    private void Shoot(Transform fireStartPosition )
    {
        _poolObjects[_currentBulletIndex].Fire(fireStartPosition);
        var time = Random.Range(_bulletData.MinDelay, _bulletData.MaxDelay);
        _timer.Init(time);
        _currentBulletIndex++;
    }

    private BaseBulletView LoadBulletView()
    {
        return ResourcesLoader.LoadAndInstantiateObject<BaseBulletView>(_prefabPath);
    }

    private Bullet CreateNewBullet()
    {
        var bullet = new Bullet(_bulletData.Speed, _bulletData.MaxDistance, LoadBulletView(), _bulletData.BulletColor);
        AddToPool(bullet);
        return bullet;
    }

    public override void UpdateTick()
    {
        for (int i = 0; i < _poolObjects.Count; i++)
        {
            _poolObjects[i].Fly();
        }
        _timer.CountTime();
    }
}
