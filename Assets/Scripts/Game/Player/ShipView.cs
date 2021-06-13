using UnityEngine;
using UnityEngine.U2D;

public class ShipView : BattleUnitView
{
    private Rigidbody2D _shipRigidBody;
    private SpriteShapeRenderer _shipShapeRenderer;

    public SpriteShapeRenderer ShipShapeRenderer => _shipShapeRenderer;
    public Rigidbody2D ShipRigidbody => _shipRigidBody;


    private void Awake()
    {
        _unitTransform = GetComponent<Transform>();
        _shipRigidBody = GetComponent<Rigidbody2D>();
        _shipShapeRenderer = GetComponent<SpriteShapeRenderer>();
    }
}
