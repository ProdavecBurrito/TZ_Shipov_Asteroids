using UnityEngine;

public class Asteroid : BaseEnemy
{
    private float _speed;

    private void Awake()
    {
        SetActivity(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collisionType = collision.GetComponent<BaseUnit>();
        {
            if (collisionType is PlayerShip || collisionType is UFO)
            {
                collisionType.GetDamage();
            }
        }
    }

    public void Fly()
    {
        if (IsActive)
        {
            transform.Translate(-Vector2.up * _speed * Time.deltaTime);
        }
        else
        {
            ReturnToPool();
        }
    }

    public void Launch(Transform startPosition)
    {
        transform.position = startPosition.position;
        transform.rotation = startPosition.rotation;
        SetActivity(true);
    }


    public void AssignSpeed(float value)
    {
        _speed = value;
    }

    public void ReturnToPool()
    {
        SetActivity(false);
    }
}
