using UnityEngine;

public class Bullet 
{
    private Vector3 _startPosition;
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
        if (BulletView.transform.position.magnitude < _maxRange)
        {
            _bulletView.transform.Translate(-Vector2.up * _speed * Time.deltaTime);
        }
        else
        {
            ReturnToPool();
        }
    }

    public void Fire(Transform fireStartTransform)
    {
        _bulletView.transform.position = fireStartTransform.position;
        _bulletView.transform.rotation = fireStartTransform.rotation;
        _startPosition = _bulletView.transform.position;
        _bulletView.ChangeActiveState(true);
        UpdatingController.SubscribeToTUpdate(Fly);
    }

    public void ReturnToPool()
    {
        _bulletView.ChangeActiveState(false);
        UpdatingController.UnsubscribeFromUpdate(Fly);
    }
}
