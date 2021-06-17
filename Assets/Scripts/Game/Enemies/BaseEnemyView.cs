using System;
using UnityEngine;

public class BaseEnemyView : BaseUnitView
{
    public event Action OnPlayerHit = delegate () { };
    
    private void Awake()
    {
        _unitTransform = GetComponent<Transform>();
        SetActivity(false);
    }

    public override void GetDamage(bool isPlayerCausedDamage)
    {
        base.GetDamage(isPlayerCausedDamage);
        SetActivity(false);
        if (isPlayerCausedDamage)
        {
            GetDamageFromPlayer();
        }
    }

    protected virtual void GetDamageFromPlayer()
    {
        OnPlayerHit.Invoke();
    }
}
