using UnityEngine;

[CreateAssetMenu(fileName = "PlayerShip", menuName = "Create/PlayerShip")]
public class ShipData : ScriptableObject
{
    public float MaxSpeed;
    public float RotationSpeed;
    public float AccelerationValue;
    public int Health;
}
