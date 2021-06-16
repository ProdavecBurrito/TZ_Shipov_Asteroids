using UnityEngine;

[CreateAssetMenu(fileName = "Asteroid" ,menuName = "Create/Asteroid")]
public class AsteroidData : ScriptableObject
{
    public AsteroidType asteroidType;
    public float MinSpeed;
    public float MaxSpeed;
    public int Score;
}
