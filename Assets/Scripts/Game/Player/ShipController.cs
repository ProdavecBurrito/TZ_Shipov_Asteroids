using System;
using UnityEngine;

public class ShipController : IUpdate, IDisposable
{
    private const int BULLET_PULL_COUNT = 15;

    private InputType _inputType;
    private BaseInput _baseInput;
    private ShipModel _shipModel;
    private ShipView _shipView;
    private BulletPool _bulletPool;
    private GameMenu _gameMenuController;
    private HealthUI _healthUI;

    public ShipController(GameMenu gameMenuController, HealthUI healthUI)
    {
        _shipView = ResourcesLoader.LoadAndInstantiateObject<ShipView>("Prefabs/Ship");
        _shipModel = new ShipModel(_shipView);
        _bulletPool = new BulletPool(BULLET_PULL_COUNT, "Data/PlayerBullet", "Prefabs/PlayerBullet");
        AssignInput(PlayerPrefs.GetInt("InputSettings"));
        _gameMenuController = gameMenuController;
        _healthUI = healthUI;
        _healthUI.GetHealth(_shipModel.Health);

        _shipView.OnHit += _shipModel.Hit;
        _gameMenuController.OnImputChange += ChangeInput;
        _shipModel.OnHealthChange += _healthUI.ReduceHealth;
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

    private void AssignInput(int inputData)
    {
        if (_shipView is ShipView shipView)
        {
            switch (inputData)
            {
                case 0:
                    _baseInput = new KeyboardInput(shipView.ShipRigidbody);
                    _inputType = InputType.Keyboard;
                    break;
                case 1:
                    _baseInput = new KeyboardPlusMouseInput(shipView.ShipRigidbody);
                    _inputType = InputType.KeyboardPlusMouse;
                    break;
            }
        }
    }

    private void ChangeInput()
    {
        if (_shipView is ShipView shipView)
        {
            switch (_inputType)
            {
                case InputType.Keyboard:
                    _baseInput = new KeyboardPlusMouseInput(shipView.ShipRigidbody);
                    _inputType = InputType.KeyboardPlusMouse;
                    Debug.Log("A");
                    break;
                case InputType.KeyboardPlusMouse:
                    _baseInput = new KeyboardInput(shipView.ShipRigidbody);
                    _inputType = InputType.Keyboard;
                    Debug.Log("B");
                    break;
            }
        }
    }

    public void Dispose()
    {
        _shipView.OnHit -= _shipModel.Hit;
        _gameMenuController.OnImputChange -= ChangeInput;
        _shipModel.OnHealthChange -= _healthUI.ReduceHealth;
    }
}