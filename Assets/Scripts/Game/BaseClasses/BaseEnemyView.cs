using System;
using UnityEngine;

public class BaseEnemyView : BaseUnitView, IScoreKeeper
{
    public event Action OnPlayerHit = delegate () { };
    
    private void Awake()
    {
        _unitTransform = GetComponent<Transform>();
        SetActivity(false);
    }

    public override void GetDamage()
    {
        base.GetDamage();
        SetActivity(false);
    }

    public void GiveScore()
    {
        OnPlayerHit.Invoke();
    }
}
