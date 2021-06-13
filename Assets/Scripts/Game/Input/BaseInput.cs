using UnityEngine;

public abstract class BaseInput
{
    protected Rigidbody2D _movingBody;

    public BaseInput(Rigidbody2D rigidbody2D)
    {
        _movingBody = rigidbody2D;
    }

    public virtual bool IsMoving()
    {
        return (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow));
    }

    public void Move(float acceleration, float maxSpeed)
    {
        _movingBody.AddRelativeForce(-Vector2.up * acceleration * Time.fixedDeltaTime, ForceMode2D.Force);
        _movingBody.velocity = Vector3.ClampMagnitude(_movingBody.velocity, maxSpeed);
    }

    public virtual bool IsShooting()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    public void Shoot(BulletPool bulletPool ,Transform fireStartPos)
    {
        bulletPool.TryShoot(fireStartPos);
    }

    public abstract bool IsRotating();

    public abstract void Rotate(float rotationSpeed);
}