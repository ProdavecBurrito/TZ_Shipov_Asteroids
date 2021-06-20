using UnityEngine;

public class UFOView : BaseEnemyView, IBattleShip
{
    private Transform _target;

    public Transform Target => _target;

    public Transform FireStartPosition {get; set;}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collisionType = GetComponent<BaseUnitView>();
        if (collisionType is ShipView || collisionType is AsteroidView)
        {
            collisionType.GetDamage();
        }
    }

    private void Awake()
    {
        _unitTransform = GetComponent<Transform>();
        _target = FindObjectOfType<ShipView>().transform;
        FireStartPosition = GetComponentInChildren<Transform>().GetChild(0);
        SetActivity(false);
    }

    public void GetTarget(ShipView shipView)
    {
        _target = shipView.transform;
    }
}
