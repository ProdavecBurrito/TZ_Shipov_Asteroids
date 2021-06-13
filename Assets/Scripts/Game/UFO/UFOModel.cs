using UnityEngine;

public class UFOModel : IScoreKeeper
{
    private UFOData _ufoData;
    private float _speed;

    public float Score { get; set; }
    public UFOData UfoData => _ufoData;
    public float Speed => _speed;

    public UFOModel()
    {
        _ufoData = ResourcesLoader.LoadObject<UFOData>("Data/UFO");
        _speed = _ufoData.Speed;
        Score = _ufoData.Score;
    }
}
