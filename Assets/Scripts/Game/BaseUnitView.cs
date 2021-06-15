using System;
using UnityEngine;

public class BaseUnitView : MonoBehaviour, IDamageable
{
    public event Action OnHit = delegate () { };

    protected Transform _unitTransform;
    private bool _isActive;

    public Transform ShipTransform => _unitTransform;
    public bool IsActive => _isActive;

    public virtual void GetDamage()
    {
        SetActivity(false);
        OnHit.Invoke();
    }

    public void SetActivity(bool isActive)
    {
        _isActive = isActive;
        _unitTransform.gameObject.SetActive(isActive);  
    }
}
