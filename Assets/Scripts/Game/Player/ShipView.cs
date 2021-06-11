using UnityEngine;
using UnityEngine.U2D;

public class ShipView : MonoBehaviour
{
    private Transform _shipTransform;
    private Rigidbody2D _shipRigidBody;
    private SpriteShapeRenderer _shipShapeRenderer;

    public Transform ShipTransform => _shipTransform;
    public Rigidbody2D ShipRigidBody => _shipRigidBody;
    public SpriteShapeRenderer ShipShapeRenderer => _shipShapeRenderer;

    private void Awake()
    {
        _shipTransform = GetComponent<Transform>();
        _shipRigidBody = GetComponent<Rigidbody2D>();
        _shipShapeRenderer = GetComponent<SpriteShapeRenderer>();
    }
}
