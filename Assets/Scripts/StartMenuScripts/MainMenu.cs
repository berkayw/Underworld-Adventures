using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private LevelLoader levelLoader;

    private void Start()
    {
        Time.timeScale = 1f;
        levelLoader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();
    }

    public void StartGame()
    {      
        levelLoader.LoadNextLevel();
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void OpenPortfolio()
    {
        Application.OpenURL("https://berkayw.github.io/portfolio-single-3.html");
    }
}
