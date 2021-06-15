using UnityEngine;
using UnityEngine.U2D;

public class ShipView : BattleUnitView
{
    private Vector2 _startPosition;
    private Rigidbody2D _shipRigidBody;
    private SpriteShapeRenderer _shipShapeRenderer;

    public SpriteShapeRenderer ShipShapeRenderer => _shipShapeRenderer;
    public Rigidbody2D ShipRigidbody => _shipRigidBody;


    private void Awake()
    {
        _startPosition = Vector2.zero;
        _unitTransform = GetComponent<Transform>();
        _shipRigidBody = GetComponent<Rigidbody2D>();
        _shipShapeRenderer = GetComponent<SpriteShapeRenderer>();
    }

    public override void GetDamage()
    {
        base.GetDamage();
        _unitTransform.position = _startPosition;
        SetActivity(true);
    }
}
