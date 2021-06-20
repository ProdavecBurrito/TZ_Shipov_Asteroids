using TMPro;

public interface IUIValue
{
    public int Value { get; }
    public TMP_Text ValueText { get; }

    public abstract void ManageValue(int newValue);
}