using System;
using UnityEngine;

public class CrackingAsteroids : Asteroid
{
    public event Action<Transform> OnCrack = delegate (Transform transform) { };

    private void Awake()
    {
        SetActivity(false);
    }

    public void Crack()
    {
        OnCrack.Invoke(transform);
    }
}
