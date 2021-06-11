using UnityEngine;

public class InputController : IUpdate
{
    private BaseInput _baseInput;
    private ShipModel _shipModel;
    private ShipView _shipView;

    public InputController(InputType inputType, ShipView shipView)
    {
        _shipView = shipView;
        _shipModel = new ShipModel();
        ChangeInput(inputType);

    }

    public void UpdateTick()
    {
        AddAcceleration(_shipModel.AccelerationValue);
        AddRotation(_shipModel.RotationSpeed);
    }

    private void AddAcceleration(float value)
    {
        if (_baseInput.IsMoving())
        {
            _baseInput.Move(_shipModel.AccelerationValue, _shipModel.MaxSpeed);
        }
    }

    private void AddRotation(float value)
    {
        if(_baseInput.IsRotating())
        {
            _baseInput.Rotate(value);
        }
    }

    public void ChangeInput(InputType inputType)
    {
        switch (inputType)
        {
            case InputType.Keyboard:
                _baseInput = new KeyboardInput(_shipView.ShipRigidBody);
                break;
            case InputType.KeyboardPlusMouse:
                _baseInput = new KeyboardPlusMouseInput(_shipView.ShipRigidBody);
                break;
        }
    }
}