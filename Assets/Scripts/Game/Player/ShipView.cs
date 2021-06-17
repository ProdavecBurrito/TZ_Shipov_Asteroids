using UnityEngine;
using UnityEngine.U2D;

public class ShipView : BaseUnitView, IBattleShip
{
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioSource _accelerationAudioSource;

    private Vector2 _startPosition;
    private Rigidbody2D _shipRigidBody;
    private SpriteShapeRenderer _shipShapeRenderer;

    public SpriteShapeRenderer ShipShapeRenderer => _shipShapeRenderer;
    public Rigidbody2D ShipRigidbody => _shipRigidBody;

    public Transform FireStartPosition { get; set; }
    public Vector2 StartPosition => _startPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collisionType = collision.GetComponent<BaseEnemyView>();
        {
            if (collisionType)
            {
                collisionType.GetDamage(true);
            }
        }
    }

    private void Awake()
    {
        _startPosition = Vector2.zero;
        _unitTransform = GetComponent<Transform>();
        _shipRigidBody = GetComponent<Rigidbody2D>();
        _shipShapeRenderer = GetComponent<SpriteShapeRenderer>();
        FireStartPosition = GetComponentInChildren<Transform>().GetChild(0);
    }

    public void SetAndPlayAudioClip(AudioClip audioClip)
    {
        _audioSource.Stop();
        _audioSource.clip = audioClip;
        _audioSource.Play();
    }

    public void LongPlay(AudioClip audioClip)
    {
        if (_accelerationAudioSource.clip == null)
        {
            _accelerationAudioSource.clip = audioClip;
        }
        if (!_accelerationAudioSource.isPlaying)
        {
            _accelerationAudioSource.Play();

        }
    }

    public void StopPlay()
    {
        _accelerationAudioSource.Stop();
    }
}
