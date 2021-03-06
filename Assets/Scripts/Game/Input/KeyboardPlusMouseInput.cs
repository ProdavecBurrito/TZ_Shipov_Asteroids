using UnityEngine;

public class KeyboardPlusMouseInput : BaseInput
{
    public KeyboardPlusMouseInput(Rigidbody2D rigidbody2D) : base(rigidbody2D)
    {
    }

    public override bool IsMoving()
    {
        return base.IsMoving() || Input.GetMouseButton(1);
    }

    public override bool IsRotating()
    {
        return true;
    }

    public override bool IsShooting()
    {
        return base.IsShooting() || Input.GetMouseButtonDown(0);
    }

    public override void Rotate(float rotationSpeed)
    {
        var direction = Input.mousePosition - Camera.main.WorldToScreenPoint(_movingBody.transform.position);
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90.0f;
        _movingBody.transform.rotation = Quaternion.Lerp(_movingBody.transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), rotationSpeed * Time.fixedDeltaTime);
    }
}