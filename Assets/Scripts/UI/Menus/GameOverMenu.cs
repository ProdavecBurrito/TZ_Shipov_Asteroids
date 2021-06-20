using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour, IInputLockMenu
{
    [SerializeField] protected Button _newGame;
    [SerializeField] protected Button _exit;

    public event Action<bool> OnMenuActive = delegate (bool isActive) { };

    private void Awake()
    {
        InitButtons();
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
        _exit.onClick.AddListener(ExitGame);
    }
}
