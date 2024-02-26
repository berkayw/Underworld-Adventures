using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public bool gameOver;

    public GameObject gameOverUI;

    public PlayerMovement player;

    public Vector3 spawnPosition;

    private PauseScreen pauseScreen;

    private void Start()
    {
        pauseScreen = FindObjectOfType<PauseScreen>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (!pauseScreen.gamePaused)
        {
            if (gameOver)
            {
                EnableGameOverUI();
                Debug.Log("Gameover Ts: " + Time.timeScale);
            }
            else
            {
                DisableGameOverUI();
                Debug.Log("Gamenotover Ts: " + Time.timeScale); 
            }
        }
    }

    public void EnableGameOverUI()
    {
        Cursor.visible = true;
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);
    }

    public void DisableGameOverUI()
    {
        Cursor.visible = false;
        Time.timeScale = 1f;
        gameOverUI.SetActive(false);
    }

    public void restartLevel()
    {
        Cursor.visible = false;
        player.health = player.maxHealth;
        player.transform.position = spawnPosition;
        gameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void backToMainMenu()
    {
        gameOver = false;
        SceneManager.LoadScene(0);

        //Destroy all DontDestroyOnLoad objects for new start
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        Destroy(GameObject.FindGameObjectWithTag("HealthBar"));
        Destroy(GameObject.FindGameObjectWithTag("GameCamera"));
        Destroy(GameObject.FindGameObjectWithTag("Crosshair"));
        Destroy(FindObjectOfType<DontDestroyBGMusic>().gameObject);
    }
}