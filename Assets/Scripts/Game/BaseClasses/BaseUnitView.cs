using System;
using UnityEngine;

public class BaseUnitView : MonoBehaviour
{
    public event Action OnHit = delegate () { };

    protected Transform _unitTransform;
    private bool _isActive;

    public Transform UnitTransform => _unitTransform;
    public bool IsActive => _isActive;

    public void SetActivity(bool isActive)
    {
        _isActive = isActive;
        _unitTransform.gameObject.SetActive(isActive); 
    }

    public virtual void GetDamage()
    {
        OnHit.Invoke();
    }
}
