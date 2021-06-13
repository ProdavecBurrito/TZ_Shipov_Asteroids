using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "Create/Bullet")]
public class BulletData : ScriptableObject
{
    public float Speed;
    public float MaxDistance;
    public float MinDelay;
    public float MaxDelay;
    public Color BulletColor;
}
