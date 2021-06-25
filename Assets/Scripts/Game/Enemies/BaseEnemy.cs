using System;
using UnityEngine;

public class BaseEnemy : BaseUnit, IScoreKeeper
{
    public event Action OnPlayerHit = delegate () { };

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
