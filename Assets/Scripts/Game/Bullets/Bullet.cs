using UnityEngine;

public class Bullet : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private float _currentPosition;
    private float _speed;
    private float _maxRange;
    private bool _isActive;
    public bool IsActive => _isActive;

    public void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        ChangeActiveState(false);
    }

    public void GetData(float speed, float maxRange, Color bulletColor)
    {
        _speed = speed;
        _maxRange = maxRange;
        SetColor(bulletColor);
    }

    public void Fly()
    {
        if (IsActive)
        {
            if (_currentPosition < _maxRange)
            {
                _currentPosition += _speed * Time.deltaTime;
                transform.Translate(-Vector2.up * _speed * Time.deltaTime);
            }
            else
            {
                ReturnToPool();
            }
        }
    }

    public void Fire(Transform fireStartPosition)
    {
        transform.position = fireStartPosition.position;
        transform.rotation = fireStartPosition.rotation;
        _currentPosition = 0;
        ChangeActiveState(true);
    }

    public void ReturnToPool()
    {
        ChangeActiveState(false);
    }

    public void ChangeActiveState(bool isActive)
    {
        gameObject.SetActive(isActive);
        _isActive = isActive;
    }

    public void SetColor(Color color)
    {
        _spriteRenderer.color = color;
    }
}
