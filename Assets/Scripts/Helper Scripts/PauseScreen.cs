using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseScreen : MonoBehaviour
{
    public GameObject pauseUI;

    public PlayerMovement player;

    public Vector3 spawnPosition;

    public bool gamePaused;
    
    private GameOverScreen gameOverScreen;

    //audio
    public AudioMixer audioMixer;
    public Slider slider;
    
    private void Start()
    {
        gameOverScreen = FindObjectOfType<GameOverScreen>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        GetVolume();
        pauseGame();

        if (!gameOverScreen.gameOver)
        {
            if (gamePaused)
            {
                EnablePauseUI();
                Debug.Log("Gamepaused Ts: " + Time.timeScale);
            }
            else
            {
                DisablePauseUI();
                Debug.Log("Gameresumed Ts: " + Time.timeScale);
            }
        }

    }

    public void EnablePauseUI()
    {
        Cursor.visible = true;
        Time.timeScale = 0f;
        pauseUI.SetActive(true);
    }

    public void DisablePauseUI()
    {
        Cursor.visible = false;
        Time.timeScale = 1f;
        pauseUI.SetActive(false);
    }

    public void restartLevel()
    {
        Cursor.visible = false;
        player.health = player.maxHealth;
        player.transform.position = spawnPosition;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void backToMainMenu()
    {
        SceneManager.LoadScene(0);
        
        //Destroy all DontDestroyOnLoad objects for new start
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        Destroy(GameObject.FindGameObjectWithTag("HealthBar"));
        Destroy(GameObject.FindGameObjectWithTag("GameCamera"));
        Destroy(GameObject.FindGameObjectWithTag("Crosshair"));
        Destroy(FindObjectOfType<DontDestroyBGMusic>().gameObject);
    }

    public void pauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gamePaused)
            {
                gamePaused = true;
            }
            else
            {
                gamePaused = false;
            }
        }
    }

    public void GetVolume()
    {
        audioMixer.GetFloat("volume", out float MV);
        slider.value = MV;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
}