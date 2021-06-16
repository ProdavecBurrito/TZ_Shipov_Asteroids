using UnityEngine;
using TMPro;

public class MainMenu : BaseMenu
{
    private void Awake()
    {
        _inputType = InputType.Keyboard;
        _inputText = _changeInput.GetComponentInChildren<TMP_Text>();
        _inputText.text = KEYBOARD;
        PlayerPrefs.SetInt("InputSettings", 0);

        InitButtons();
    }
}
