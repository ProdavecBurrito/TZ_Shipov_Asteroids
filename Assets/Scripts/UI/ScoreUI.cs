using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;

    private int _score;

    private void Awake()
    {
        _score = 0;
        _scoreText = GetComponentInChildren<TMP_Text>();
    }

    public void AddScore(int value)
    {
        _score += value;
        _scoreText.text = $"Score: {_score}";
    }
}
