using UnityEngine;

[CreateAssetMenu(fileName = "UFO", menuName = "Create/UFO")]
public class UFOData : ScriptableObject
{
    public float Speed;
    public int Score;
    public int MinSpawnTime;
    public int MaxSpawnTime;
}
