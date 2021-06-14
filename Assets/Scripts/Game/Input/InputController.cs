using System;
using UnityEngine;

public class InputController : IUpdate, IDisposable
{
    private const int BULLET_PULL_COUNT = 5;

    private InputType _inputType;
    private BaseInput _baseInput;
    private ShipModel _shipModel;
    private BattleUnitView _shipView;
    private BulletPool _bulletPool;
    private GameMenuController _gameMenuController;

    public InputController(BattleUnitView shipView, GameMenuController gameMenuController)
    {
        _shipView = shipView;
        _shipModel = new ShipModel();
        _bulletPool = new BulletPool(BULLET_PULL_COUNT, "Data/PlayerBullet");
        AssignInput(PlayerPrefs.GetInt("InputSettings"));
        _gameMenuController = gameMenuController;
        _gameMenuController.OnImputChange += ChangeInput;
    }

    public void UpdateTick()
    {
        AddAcceleration(_shipModel.AccelerationValue);
        AddRotation(_shipModel.RotationSpeed);
        Shoot();
        OpenMenu();
        _bulletPool.UpdateTick();
    }

    private void OpenMenu()
    {
        _baseInput.OpenMenu(_gameMenuController);
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

    public void AssignInput(int inputData)
    {
        if (_shipView is ShipView shipView)
        {
            switch (inputData)
            {
                case 0:
                    _baseInput = new KeyboardInput(shipView.ShipRigidbody);
                    break;
                case 1:
                    _baseInput = new KeyboardPlusMouseInput(shipView.ShipRigidbody);
                    break;
            }
        }
    }

    public void ChangeInput()
    {
        if (_shipView is ShipView shipView)
        {
            switch (_inputType)
            {
                case InputType.Keyboard:
                    _baseInput = new KeyboardPlusMouseInput(shipView.ShipRigidbody);
                    break;
                case InputType.KeyboardPlusMouse:
                    _baseInput = new KeyboardInput(shipView.ShipRigidbody);
                    break;
            }
        }
    }

    public void Dispose()
    {
        _gameMenuController.OnImputChange -= ChangeInput;
    }
}