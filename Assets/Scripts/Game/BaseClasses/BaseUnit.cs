using System;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public event Action OnHit = delegate () { };

    private bool _isActive;
    public bool IsActive => _isActive;

    public void SetActivity(bool isActive)
    {
        _isActive = isActive;
        gameObject.SetActive(isActive); 
    }

    public virtual void GetDamage()
    {
        OnHit.Invoke();
    }
}
