using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _healthText;

    private int _health;

    private void Awake()
    {
        _health = 3;
        _healthText = GetComponentInChildren<TMP_Text>();
    }

    public void GetHealth(int healthValue)
    {
        _health = healthValue;
        ChangeHealthText();
    }

    public void ReduceHealth()
    {
        _health--;
        ChangeHealthText();
    }

    private void ChangeHealthText()
    {
        _healthText.text = $"Health: {_health}";
    }
}
