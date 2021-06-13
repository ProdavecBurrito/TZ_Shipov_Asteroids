using UnityEngine;
using UnityEngine.U2D;

public class ShipView : MonoBehaviour, IDamageble
{
    [SerializeField] private Transform _fireStartPosition;

    private Transform _shipTransform;
    private Rigidbody2D _shipRigidBody;
    private SpriteShapeRenderer _shipShapeRenderer;

    public Transform ShipTransform => _shipTransform;
    public Rigidbody2D ShipRigidBody => _shipRigidBody;
    public SpriteShapeRenderer ShipShapeRenderer => _shipShapeRenderer;
    public Transform FireStartPosition => _fireStartPosition;

    public void GetDamage()
    {
        throw new System.NotImplementedException();
    }

    private void Awake()
    {
        _shipTransform = GetComponent<Transform>();
        _shipRigidBody = GetComponent<Rigidbody2D>();
        _shipShapeRenderer = GetComponent<SpriteShapeRenderer>();
    }
}
