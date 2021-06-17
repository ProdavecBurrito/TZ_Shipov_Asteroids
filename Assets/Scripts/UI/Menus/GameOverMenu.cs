using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] protected Button _newGame;
    [SerializeField] protected Button _exit;

    private void Awake()
    {
        InitButtons();
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
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
