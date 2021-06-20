using UnityEngine;

public interface ISpawner
{
    public float SpawnHight { get;}
    public float SpawnWidth { get;}

    public abstract Vector2 CalculateStartPosition();
}