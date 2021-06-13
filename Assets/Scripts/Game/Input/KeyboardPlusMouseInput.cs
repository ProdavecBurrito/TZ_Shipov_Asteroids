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
        return base.IsShooting() || Input.GetMouseButton(0);
    }

    public override void Rotate(float rotationSpeed)
    {
        var direction = Input.mousePosition - Camera.main.WorldToScreenPoint(_movingBody.transform.position);
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _movingBody.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}