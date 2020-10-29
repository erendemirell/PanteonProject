using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNextLevel()
    {
        SceneManager.LoadScene("GameSceneOpponent");
    }

    public void RetryLevel()
    {
        Time.timeScale = 1;

        int currentScene = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
