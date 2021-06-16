using System;

public class ShipModel
{
    public event Action OnHealthChange = delegate () { };
    public event Action OnDie = delegate () { };

    private ShipData _shipData;
    private Invincibility _invincibility;

    private int _health;
    private float _maxSpeed;
    private float _rotationSpeed;
    private float _accelerationValue;

    public float MaxSpeed => _maxSpeed;
    public float RotationSpeed => _rotationSpeed;
    public float AccelerationValue => _accelerationValue;
    public int Health => _health;

    public ShipModel(BaseUnitView shipView)
    {
        _shipData = ResourcesLoader.LoadObject<ShipData>("Data/PlayerShip");
        _invincibility = new Invincibility(shipView);
        _maxSpeed = _shipData.MaxSpeed;
        _rotationSpeed = _shipData.RotationSpeed;
        _accelerationValue = _shipData.AccelerationValue;
        _health = _shipData.Health;
    }

    public void Hit()
    {
        if (!_invincibility.IsInvincible)
        {
            _health--;
            if (_health > 0)
            {
                OnHealthChange.Invoke();
                _invincibility.StartInvincibility();
            }
            else
            {
                OnDie.Invoke();
            }
        }
    }
}
