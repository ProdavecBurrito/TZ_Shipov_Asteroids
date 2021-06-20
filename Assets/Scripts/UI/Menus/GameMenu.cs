using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : BaseMenu, IInputLockMenu
{
    public event Action OnImputChange = delegate () { };
    public event Action<bool> OnMenuActive = delegate (bool isActive) { };

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
        OnMenuActive.Invoke(true);
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
        OnMenuActive.Invoke(false);
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
