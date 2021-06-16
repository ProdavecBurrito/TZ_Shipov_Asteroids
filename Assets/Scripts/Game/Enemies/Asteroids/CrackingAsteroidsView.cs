using System;
using UnityEngine;

public class CrackingAsteroidsView : BaseEnemyView
{
    public event Action OnCrack = delegate () { };

    private void Awake()
    {
        _unitTransform = GetComponent<Transform>();
        SetActivity(false);
    }

    public override void GetDamage(bool isPlayerCausedDamage)
    {
        base.GetDamage(isPlayerCausedDamage);
        if (isPlayerCausedDamage)
        {
            OnCrack.Invoke();
        }
    }
}
