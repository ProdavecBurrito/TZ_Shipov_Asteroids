using UnityEngine;

public class InputController : IUpdate
{
    private const int BULLET_PULL_COUNT = 5;

    private BaseInput _baseInput;
    private ShipModel _shipModel;
    private BattleUnitView _shipView;
    private BulletPool _bulletPool;

    public InputController(InputType inputType, BattleUnitView shipView)
    {
        _shipView = shipView;
        _shipModel = new ShipModel();
        _bulletPool = new BulletPool(BULLET_PULL_COUNT, "Data/PlayerBullet");
        ChangeInput(inputType);

    }

    public void UpdateTick()
    {
        AddAcceleration(_shipModel.AccelerationValue);
        AddRotation(_shipModel.RotationSpeed);
        Shoot();
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

    private void Shoot()
    {
        if (_baseInput.IsShooting())
        {
            _baseInput.Shoot(_bulletPool, _shipView.FireStartPosition);
        }
    }

    public void ChangeInput(InputType inputType)
    {
        if (_shipView is ShipView shipView)
        {
            switch (inputType)
            {
                case InputType.Keyboard:
                    _baseInput = new KeyboardInput(shipView.ShipRigidbody);
                    break;
                case InputType.KeyboardPlusMouse:
                    _baseInput = new KeyboardPlusMouseInput(shipView.ShipRigidbody);
                    break;
            }
        }
    }
}