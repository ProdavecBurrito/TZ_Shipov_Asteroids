using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : BaseMenu
{
    public event Action OnImputChange = delegate () { };

    [SerializeField] private Button _continue;

    private void Awake()
    {
        InitButtons();
        _inputType = (InputType)PlayerPrefs.GetInt("InputSettings");
        _inputText = _changeInput.GetComponentInChildren<TMP_Text>();
        AssignStartInputText(PlayerPrefs.GetInt("InputSettings"));
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    private void Continue()
    {
        gameObject.SetActive(false);
    }

    protected override void ChangeInput()
    {
        base.ChangeInput();
        OnImputChange.Invoke();
    }

    protected override void InitButtons()
    {
        base.InitButtons();
        _continue.onClick.AddListener(Continue);
    }
}
