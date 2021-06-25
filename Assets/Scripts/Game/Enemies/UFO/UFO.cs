using UnityEngine;

public class UFO : BaseEnemy, IUpdate
{
    private const int BULLET_PULL_COUNT = 5;
    private const float ROTATION_ÑOMPENSATION = 90.0f;

    private BulletPool _bulletPool;
    private AudioSource _audioSource;
    private GameObject navigationGO;
    private ScoreUI _scoreUI;
    private Transform _target;
    private UFOData _ufoData;
    private AudioClip _explosion;
    private AudioClip _fire;

    public UFOData UfoData => _ufoData;
    public Transform FireStartPosition { get; private set; }
    public int Score { get; private set; }

    private int _direction = 1;

    private void Awake()
    {
        _bulletPool = new BulletPool(BULLET_PULL_COUNT, "Data/UFOBullet", "Prefabs/UFOBullet");
        navigationGO = new GameObject();
        navigationGO.AddComponent(typeof(AudioSource));

        _ufoData = ResourcesLoader.LoadObject<UFOData>("Data/UFO");
        _explosion = ResourcesLoader.LoadObject<AudioClip>("UfoExplosion");
        _fire = ResourcesLoader.LoadObject<AudioClip>("UfoFire");

        _audioSource = navigationGO.GetComponent<AudioSource>();
        _audioSource.volume = 0.5f;
        _audioSource.clip = _explosion;

        _target = FindObjectOfType<PlayerShip>().transform;
        FireStartPosition = GetComponentInChildren<Transform>().GetChild(0);

        Score = _ufoData.Score;

        _bulletPool.OnFire += PlayFireSound;
        OnHit += PlayExplosionSound;
        OnPlayerHit += AddScore;
    }

    public void InjectUI(IUIValue scoreUI)
    {
        _scoreUI = (ScoreUI)scoreUI;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collisionType = GetComponent<BaseUnit>();
        if (collisionType is PlayerShip || collisionType is Asteroid)
        {
            collisionType.GetDamage();
        }
    }

    public void UpdateTick()
    {
        ExecuteUpdateTick();
        _bulletPool.UpdateTick();
    }

    public void AddScore()
    {
        _scoreUI.ManageValue(Score);
    }

    private void ExecuteUpdateTick()
    {
        if (IsActive)
        {
            FolowTarget();
            Move();
            TryToShoot();
        }
    }

    public void GetDirection(int value)
    {
        _direction = value;
    }

    private void Move()
    {
        transform.Translate(Vector2.right * _direction * _ufoData.Speed * Time.deltaTime);
    }

    private void TryToShoot()
    {
        _bulletPool.TryToAct(FireStartPosition);
    }

    private void FolowTarget()
    {
        var direction = _target.position - transform.position;
        var position = -(transform.position - _target.position).normalized;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + ROTATION_ÑOMPENSATION;
        FireStartPosition.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        FireStartPosition.localPosition = position;
    }

    private void PlayFireSound()
    {
        _audioSource.clip = _fire;
        _audioSource.Play();
    }

    private void PlayExplosionSound()
    {
        _audioSource.clip = _explosion;
        _audioSource.Play();
    }

    private void OnDestroy()
    {
        _bulletPool.OnFire -= PlayFireSound;
        OnHit -= PlayExplosionSound;
        OnPlayerHit -= AddScore;
    }
}
