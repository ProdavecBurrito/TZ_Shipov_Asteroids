using System;
using UnityEngine;

public class ShipModel
{
    public event Action OnHealthChange = delegate () { };
    public event Action OnDie = delegate () { };

    private ShipData _shipData;

    private int _health;
    private float _maxSpeed;
    private float _rotationSpeed;
    private float _accelerationValue;
    private AudioClip _fireSound;
    private AudioClip _accelerationSound;
    private AudioClip _dieSound;

    public float MaxSpeed => _maxSpeed;
    public float RotationSpeed => _rotationSpeed;
    public float AccelerationValue => _accelerationValue;
    public int Health => _health;

    public AudioClip FireSound => _fireSound;
    public AudioClip AccelerationSound => _accelerationSound;
    public AudioClip DieSound => _dieSound;

    public ShipModel(BaseUnitView shipView)
    {
        _shipData = ResourcesLoader.LoadObject<ShipData>("Data/PlayerShip");
        _maxSpeed = _shipData.MaxSpeed;
        _rotationSpeed = _shipData.RotationSpeed;
        _accelerationValue = _shipData.AccelerationValue;
        _health = _shipData.Health;

        _fireSound = ResourcesLoader.LoadObject<AudioClip>("PlayerShipFire");
        _accelerationSound = ResourcesLoader.LoadObject<AudioClip>("PlayerShipAcceleration");
        _dieSound = ResourcesLoader.LoadObject<AudioClip>("PlayerShipExplosion");
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
}
