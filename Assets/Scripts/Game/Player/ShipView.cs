using UnityEngine;
using UnityEngine.U2D;

public class ShipView : BaseUnitView, IBattleShip
{
    private Vector2 _startPosition;
    private Rigidbody2D _shipRigidBody;
    private SpriteShapeRenderer _shipShapeRenderer;

    public SpriteShapeRenderer ShipShapeRenderer => _shipShapeRenderer;
    public Rigidbody2D ShipRigidbody => _shipRigidBody;

    public Transform FireStartPosition { get; set; }

    private void Awake()
    {
        _startPosition = Vector2.zero;
        _unitTransform = GetComponent<Transform>();
        _shipRigidBody = GetComponent<Rigidbody2D>();
        _shipShapeRenderer = GetComponent<SpriteShapeRenderer>();
        FireStartPosition = GetComponentInChildren<Transform>().GetChild(0);
    }

    public override void GetDamage(bool isPlayerCausedDamage)
    {
        base.GetDamage(isPlayerCausedDamage);
        _unitTransform.position = _startPosition;
        SetActivity(true);
    }
}
