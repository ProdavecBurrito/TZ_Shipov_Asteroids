using UnityEngine;

public class UFOView : BattleUnitView
{
    private Transform _target;

    public Transform Target => _target;

    private void Awake()
    {
        _unitTransform = GetComponent<Transform>();
        SetActivity(false);
        _target = FindObjectOfType<ShipView>().ShipTransform;
    }
}
