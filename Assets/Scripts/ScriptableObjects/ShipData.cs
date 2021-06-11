using UnityEngine;

[CreateAssetMenu(fileName = "PlayerShip", menuName = "MainShip/Create")]
public class ShipData : ScriptableObject
{
    public float MaxSpeed;
    public float RotationSpeed;
    public float AccelerationValue;
    public int Health;
}
