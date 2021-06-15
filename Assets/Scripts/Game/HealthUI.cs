using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;

    private int _health;

    private void Awake()
    {
        _health = 3;
        _scoreText = GetComponentInChildren<TMP_Text>();
    }

    public void ReduceScore()
    {
        _health--;
        _scoreText.text = $"Health: {_health}";
    }
}
