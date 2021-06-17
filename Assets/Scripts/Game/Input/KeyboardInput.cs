using UnityEngine;

public class KeyboardInput : BaseInput
{
    private const float СOMPENSATE_ROTATION_SPEED = 30.0f;

    public KeyboardInput(Rigidbody2D rigidbody2D) : base(rigidbody2D)
    {
    }

    public override bool IsRotating()
    {
        return Input.GetAxis("Horizontal") != 0;
    }

    public override void Rotate(float rotationSpeed)
    {
        _movingBody.rotation += -Input.GetAxis("Horizontal") * (rotationSpeed * СOMPENSATE_ROTATION_SPEED) * Time.fixedDeltaTime;
    }
}