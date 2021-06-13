using UnityEngine;

public class BattleUnitView : BaseUnitView
{
    [SerializeField] protected Transform _fireStartPositions;

    public Transform FireStartPosition => _fireStartPositions;
}