using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour, IUIValue
{
    [SerializeField] public TMP_Text ValueText { get; private set; }

    public int Value { get; private set ; }

    private void Awake()
    {
        Value = 0;
        ValueText = GetComponentInChildren<TMP_Text>();
    }

    public void ManageValue(int newValue)
    {
        Value += newValue;
        ValueText.text = $"Score: {Value}";
    }
}
