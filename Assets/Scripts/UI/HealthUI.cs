using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour, IUIValue
{
    [SerializeField] private TMP_Text _healthText;

    public int Value { get; private set; }
    public TMP_Text ValueText { get; private set ; }

    private void Awake()
    {
        Value = 3;
        _healthText = GetComponentInChildren<TMP_Text>();
    }

    public void ReduceHealth()
    {
        Value--;
        ChangeHealthText();
    }

    private void ChangeHealthText()
    {
        _healthText.text = $"Health: {Value}";
    }

    public void ManageValue(int newValue)
    {
        Value = newValue;
        ChangeHealthText();
    }
}
