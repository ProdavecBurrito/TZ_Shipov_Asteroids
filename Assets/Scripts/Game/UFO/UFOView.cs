using UnityEngine;

public class UFOView : BattleUnitView
{
    private Transform _target;
    private Vector3 _offset;

    public Transform Target => _target;
    public Vector3 Offset => _offset;

    private void Awake()
    {
        _unitTransform = GetComponent<Transform>();
        SetActivity(false);
        _offset = _fireStartPositions.localPosition - _unitTransform.localPosition;
    }

    public void GetTarget(ShipView shipView)
    {
        _target = shipView.transform;
    }
}
