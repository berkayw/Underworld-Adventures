using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject currentDoor;
    public int doorNumber;
    private GameObject levelLoader;
    public int currentKills;

    private GameObject player;

    private Camera mainCam;

    public Vector3 nextLevelSpawnPoint;
    public Color32 nextLevelBackgroundColor;
    
    public Vector3 currentLevelSpawnPoint;
    //public Color32 nextLevelBackgroundColor;

    public AudioClip levelCompletedSE;
    
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        mainCam = Camera.main;
        levelLoader = GameObject.FindWithTag("LevelLoader");
    }

    private void Update()
    {
        currentDoor = GameObject.FindGameObjectWithTag("LevelDoor" + doorNumber); //Get active door/chapter

        if (currentDoor.GetComponent<Door>().isFinalDoor && !currentDoor.GetComponent<BoxCollider2D>().enabled &&
            SceneManager.GetActiveScene().buildIndex + 1 != null)
        {
            if (SceneManager.GetActiveScene().buildIndex + 1 == 5)
            {
                endGame();
                Cursor.visible = true;
            }
            else
            {
                LoadNextLevel();
            }
        }
    }

    public void EnemyKilled()
    {
        currentKills++;

        if (currentKills == currentDoor.GetComponent<Door>().requiredKills)
        {
            currentDoor.GetComponent<Door>().playlevelDoorSE();
            currentDoor.GetComponent<Animator>().SetBool("isDoorOpen", true);
            Invoke("openRoom", 0.8f);
        }
    }

    public void LoadNextLevel()
    {
        AudioSource.PlayClipAtPoint(levelCompletedSE, transform.position, 0.1f);
        levelLoader.GetComponent<LevelLoader>().LoadNextLevel();
        Invoke("setupNextLevel", 1f);
    }

    public void setupNextLevel()
    {
        player.transform.position = nextLevelSpawnPoint;
        mainCam.backgroundColor = nextLevelBackgroundColor;
        currentKills = 0;
        doorNumber = 0;
    }

    public void openRoom()
    {
        currentDoor.GetComponent<BoxCollider2D>().enabled = false;
        if (!currentDoor.GetComponent<Door>().isFinalDoor)
        {
            doorNumber++;
        }
    }

    public void endGame()
    {
        SceneManager.LoadScene(0);
        
        //Destroy all DontDestroyOnLoad objects for new start
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        Destroy(GameObject.FindGameObjectWithTag("HealthBar"));
        Destroy(GameObject.FindGameObjectWithTag("GameCamera"));
        Destroy(GameObject.FindGameObjectWithTag("Crosshair"));
        Destroy(FindObjectOfType<DontDestroyBGMusic>().gameObject);
    }
    
}