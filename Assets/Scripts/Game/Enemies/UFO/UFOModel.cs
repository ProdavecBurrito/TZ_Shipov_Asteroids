using UnityEngine;

public class UFOModel 
{
    private UFOData _ufoData;
    private AudioClip _explosionClip;
    private AudioClip _fireClip;

    private float _speed;

    public int Score { get; set; }
    public UFOData UfoData => _ufoData;
    public float Speed => _speed;

    public AudioClip ExplosionClip => _explosionClip;
    public AudioClip FireClip => _fireClip;

    public UFOModel()
    {
        _ufoData = ResourcesLoader.LoadObject<UFOData>("Data/UFO");
        _speed = _ufoData.Speed;
        Score = _ufoData.Score;

        _explosionClip = ResourcesLoader.LoadObject<AudioClip>("UfoExplosion");
        _fireClip = ResourcesLoader.LoadObject<AudioClip>("UfoFire");
    }
}
