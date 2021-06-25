using System;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerShip : BaseUnit, IUpdate, IFixedUpdate
{
    private const int BULLET_PULL_COUNT = 15;

    public event Action OnHealthChange = delegate () { };
    public event Action OnDie = delegate () { };

    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioSource _accelerationAudioSource;

    private InputType _inputType;

    private BaseInput _currentInput;
    private KeyboardPlusMouseInput _keyboardPlusMouseInput;
    private KeyboardInput _keyboardInput;
    private BulletPool _bulletPool;
    private GameMenu _gameMenuController;
    private GameOverMenu _gameOver;
    private HealthUI _healthUI;
    private Invincibility _invincibility;
    private ShipData _shipData;
    private AudioClip _fireSound;
    private AudioClip _accelerationSound;
    private AudioClip _dieSound;
    private Rigidbody2D _shipRigidBody;
    private SpriteShapeRenderer _shipShapeRenderer;
    private Vector2 _startPosition;

    private int _health;
    private bool IsMoving;
    private bool IsRotating;

    public Transform FireStartPosition { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collisionType = collision.GetComponent<BaseEnemy>();
        {
            if (collisionType)
            {
                collisionType.GetDamage();
            }
            if (collisionType is IScoreKeeper)
            {
                collisionType.GiveScore();
            }
        }
    }

    private void Awake()
    {
        _startPosition = Vector2.zero;
        _shipRigidBody = GetComponent<Rigidbody2D>();
        _shipShapeRenderer = GetComponent<SpriteShapeRenderer>();
        FireStartPosition = GetComponentInChildren<Transform>().GetChild(0);

        _invincibility = new Invincibility(_shipShapeRenderer);
        _keyboardPlusMouseInput = new KeyboardPlusMouseInput(_shipRigidBody);
        _keyboardInput = new KeyboardInput(_shipRigidBody);
        _bulletPool = new BulletPool(BULLET_PULL_COUNT, "Data/PlayerBullet", "Prefabs/PlayerBullet");

        _shipData = ResourcesLoader.LoadObject<ShipData>("Data/PlayerShip");
        _fireSound = ResourcesLoader.LoadObject<AudioClip>("PlayerShipFire");
        _accelerationSound = ResourcesLoader.LoadObject<AudioClip>("PlayerShipAcceleration");
        _dieSound = ResourcesLoader.LoadObject<AudioClip>("PlayerShipExplosion");

        _health = _shipData.Health;
        AssignInput(PlayerPrefs.GetInt("InputSettings"));
        LockInput(false);

        _bulletPool.OnFire += PlayFireSound;
    }

    public void InjectUI(GameMenu gameMenuController, GameOverMenu gameOverMenu, IUIValue healthUI)
    {
        _gameMenuController = gameMenuController;
        _gameOver = gameOverMenu;
        _healthUI = (HealthUI)healthUI;

        _gameMenuController.OnImputChange += ChangeInput;
        _gameMenuController.OnMenuActive += LockInput;
        _gameOver.OnMenuActive += LockInput;
        _healthUI.ManageValue(_health);
                OnHealthChange += _healthUI.ReduceHealth;
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
            LongPlay(_accelerationSound);
            IsMoving = true;
        }
        else
        {
            StopAccelerationPlay();
            IsMoving = false;
        }
    }

    private void Move()
    {
        if (IsMoving)
        {
            _currentInput.Move(_shipData.AccelerationValue, _shipData.MaxSpeed);
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
            _currentInput.Rotate(_shipData.RotationSpeed);
        }
    }

    private void Shoot()
    {
        if (_currentInput.IsShooting())
        {
            _currentInput.Shoot(_bulletPool, FireStartPosition);
        }
    }

    public override void GetDamage()
    {
        base.GetDamage();
        GetHit();
    }

    private void GetHit()
    {
        if (!_invincibility.IsInvincible)
        {
            SetAndPlayAudioClip(_dieSound);
            transform.position = _startPosition;
            _invincibility.StartInvincibility();
            _shipRigidBody.velocity = Vector2.zero;
            Hit();
        }
    }

    public void Hit()
    {
        _health--;
        OnHealthChange.Invoke();
        if (_health <= 0)
        {
            OnDie.Invoke();
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


    private void PlayFireSound()
    {
        SetAndPlayAudioClip(_fireSound);
    }

    private void SetAndPlayAudioClip(AudioClip audioClip)
    {
        _audioSource.Stop();
        _audioSource.clip = audioClip;
        _audioSource.Play();
    }

    private void LongPlay(AudioClip audioClip)
    {
        if (_accelerationAudioSource.clip == null)
        {
            _accelerationAudioSource.clip = audioClip;
        }
        if (!_accelerationAudioSource.isPlaying)
        {
            _accelerationAudioSource.Play();

        }
    }

    private void StopAccelerationPlay()
    {
        _accelerationAudioSource.Stop();
    }

    private void OnDestroy()
    {
        _gameMenuController.OnImputChange -= ChangeInput;
        _gameMenuController.OnMenuActive -= _currentInput.LockInput;
        _gameOver.OnMenuActive -= _currentInput.LockInput;
        _bulletPool.OnFire -= PlayFireSound;
        OnHealthChange -= _healthUI.ReduceHealth;
    }
}