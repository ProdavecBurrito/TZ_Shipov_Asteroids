using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceenLoader : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
