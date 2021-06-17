using System;
using UnityEngine;

public class CrackingAsteroidsView : AsteroidView
{
    public event Action<Transform> OnCrack = delegate (Transform transform) { };

    private void Awake()
    {
        _unitTransform = GetComponent<Transform>();
        SetActivity(false);
    }

    public void Crack()
    {
        OnCrack.Invoke(UnitTransform);
    }
}
