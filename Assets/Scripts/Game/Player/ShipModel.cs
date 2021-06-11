using System;

public class ShipModel
{
    public event Action OnHit = delegate () { };

    private ShipData _shipData;

    private int _health;
    private float _maxSpeed;
    private float _rotationSpeed;
    private float _accelerationValue;

    public float MaxSpeed => _maxSpeed;
    public float RotationSpeed => _rotationSpeed;
    public float AccelerationValue => _accelerationValue;

    public ShipModel()
    {
        _shipData = ResourcesLoader.LoadObject<ShipData>("Data/PlayerShip");
        _maxSpeed = _shipData.MaxSpeed;
        _rotationSpeed = _shipData.RotationSpeed;
        _accelerationValue = _shipData.AccelerationValue;
        _health = _shipData.Health;
    }

    public void Hit()
    {
        if (_health > 0)
        {
            _health--;
            OnHit.Invoke();
        }
    }
}
