using UnityEngine;

public class KeyboardInput : BaseInput
{
    public KeyboardInput(Rigidbody2D rigidbody2D) : base(rigidbody2D)
    {
    }

    public override bool IsRotating()
    {
        return Input.GetAxis("Horizontal") != 0;
    }

    public override void Rotate(float rotationSpeed)
    {
        _movingBody.rotation += -Input.GetAxis("Horizontal") * rotationSpeed * Time.fixedDeltaTime;
    }
}