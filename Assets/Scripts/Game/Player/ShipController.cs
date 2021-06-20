using System;
using UnityEngine;

public class ShipController : IUpdate, IFixedUpdate, IDisposable
{
    public event Action OnHitTrue = delegate () { };

    private const int BULLET_PULL_COUNT = 15;

    private InputType _inputType;

    private BaseInput _currentInput;
    private KeyboardPlusMouseInput _keyboardPlusMouseInput;
    private KeyboardInput _keyboardInput;

    private ShipModel _shipModel;
    private ShipView _shipView;
    private BulletPool _bulletPool;
    private GameMenu _gameMenuController;
    private GameOverMenu _gameOver;
    private HealthUI _healthUI;
    private Invincibility _invincibility;

    private bool IsMoving;
    private bool IsRotating;

    public ShipModel ShipModel => _shipModel;

    public ShipController(GameMenu gameMenuController, GameOverMenu gameOverMenu, IUIValue healthUI)
    {

        _shipView = ResourcesLoader.LoadAndInstantiateObject<ShipView>("Prefabs/Ship");
        _invincibility = new Invincibility(_shipView);
        _shipModel = new ShipModel(_shipView);
        _keyboardPlusMouseInput = new KeyboardPlusMouseInput(_shipView.ShipRigidbody);
        _keyboardInput = new KeyboardInput(_shipView.ShipRigidbody);

        _bulletPool = new BulletPool(BULLET_PULL_COUNT, "Data/PlayerBullet", "Prefabs/PlayerBullet");
        AssignInput(PlayerPrefs.GetInt("InputSettings"));
        _gameMenuController = gameMenuController;
        _gameOver = gameOverMenu;
        _healthUI = (HealthUI)healthUI;
        _healthUI.ManageValue(_shipModel.Health);
        LockInput(false);

        OnHitTrue += _shipModel.Hit;
        _shipView.OnHit += GetHit;
        _gameMenuController.OnImputChange += ChangeInput;
        _gameMenuController.OnMenuActive += LockInput;
        _gameOver.OnMenuActive += LockInput;
        _shipModel.OnHealthChange += _healthUI.ReduceHealth;
        _bulletPool.OnFire += PlayFireSound;

    }

    public void FixedUpdateTick()
    {
        Move();
        Rotate();
    }
    public void UpdateTick()
    {
        CheckInputLock();
        OpenMenu();
        _invincibility.UpdateTick();
        _bulletPool.UpdateTick();
    }

    private void CheckInputLock()
    {
        if (!_currentInput.IsLocked)
        {
            AddAcceleration();
            AddRotation();
            Shoot();
        }
    }

    private void OpenMenu()
    {
        _currentInput.OpenMenu(_gameMenuController);
    }

    private void AddAcceleration()
    {
        if (_currentInput.IsMoving())
        {
            _shipView.LongPlay(_shipModel.AccelerationSound);
            IsMoving = true;
        }
        else
        {
            _shipView.StopPlay();
            IsMoving = false;
        }
    }

    private void Move()
    {
        if (IsMoving)
        {
            _currentInput.Move(_shipModel.AccelerationValue, _shipModel.MaxSpeed);
        }
    }

    private void AddRotation()
    {
        if(_currentInput.IsRotating())
        {
            IsRotating = true;
        }
        else
        {
            IsRotating = false;
        }
    }

    private void Rotate()
    {
        if (IsRotating)
        {
            _currentInput.Rotate(_shipModel.RotationSpeed);
        }
    }

    private void Shoot()
    {
        if (_currentInput.IsShooting())
        {
            _currentInput.Shoot(_bulletPool, _shipView.FireStartPosition);
        }
    }

    private void PlayFireSound()
    {
        _shipView.SetAndPlayAudioClip(_shipModel.FireSound);
    }

    private void GetHit()
    {
        if (!_invincibility.IsInvincible)
        {
            _shipView.SetAndPlayAudioClip(_shipModel.DieSound);
            _shipView.UnitTransform.position = _shipView.StartPosition;
            _invincibility.StartInvincibility();
            _shipView.ShipRigidbody.velocity = Vector2.zero;
            OnHitTrue.Invoke();
        }
    }

    private void AssignInput(int inputData)
    {
        switch (inputData)
        {
            case 0:
                _currentInput = _keyboardInput;
                _inputType = InputType.Keyboard;
                break;
            case 1:
                _currentInput = _keyboardPlusMouseInput;
                _inputType = InputType.KeyboardPlusMouse;
                break;
        }
    }

    private void ChangeInput()
    {
        switch (_inputType)
        {
            case InputType.Keyboard:
                _currentInput = _keyboardPlusMouseInput;
                LockInput(true);
                _inputType = InputType.KeyboardPlusMouse;
                break;
            case InputType.KeyboardPlusMouse:
                _currentInput = _keyboardInput;
                LockInput(true);
                _inputType = InputType.Keyboard;
                break;
        }
    }

    private void LockInput(bool isLocked)
    {
        _currentInput.LockInput(isLocked);
    }

    public void Dispose()
    {
        OnHitTrue -= _shipModel.Hit;
        _shipView.OnHit -= GetHit;
        _gameMenuController.OnImputChange -= ChangeInput;
        _gameMenuController.OnMenuActive -= _currentInput.LockInput;
        _gameOver.OnMenuActive -= _currentInput.LockInput;
        _shipModel.OnHealthChange -= _healthUI.ReduceHealth;
        _bulletPool.OnFire -= PlayFireSound;
    }
}