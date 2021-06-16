using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public abstract class BaseMenu : MonoBehaviour
{
    protected const string KEYBOARD = "Control: Keybord";
    protected const string KEY_PLUS_MOUSE = "Control: Keybord Plus Mouse";

    protected InputType _inputType;
    protected TMP_Text _inputText;

    [SerializeField] protected Button _newGame;
    [SerializeField] protected Button _changeInput;
    [SerializeField] protected Button _exit;

    protected virtual void ChangeInput()
    {
        if (_inputType == InputType.Keyboard)
        {
            PlayerPrefs.SetInt("InputSettings", 1);
            _inputText.text = KEY_PLUS_MOUSE;
            _inputType = (InputType)1;
        }
        else
        {
            PlayerPrefs.SetInt("InputSettings", 0);
            _inputText.text = KEYBOARD;
            _inputType = (InputType)0;
        }
    }

    protected void AssignStartInputText(int inputType)
    {
        if (inputType == 0)
        {
            _inputText.text = KEYBOARD;
        }
        else
        {
            _inputText.text = KEY_PLUS_MOUSE;
        }
    }

    protected void ExitGame()
    {
        Application.Quit();
    }

    protected void LoadNewGame()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    protected virtual void InitButtons()
    {
        _newGame.onClick.AddListener(LoadNewGame);
        _changeInput.onClick.AddListener(ChangeInput);
        _exit.onClick.AddListener(ExitGame);
    }
}
