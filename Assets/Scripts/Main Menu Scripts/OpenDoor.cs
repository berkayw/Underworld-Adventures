using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDoor : MonoBehaviour
{
    private Animator anim;
    private bool doorOpened;
    public bool characterSelected;
    public GameObject selectedCharacter;
    public GameObject levelLoader;
    public AudioClip doorOpenAudio;
    private bool soundEffectPlayed;
    
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
        levelLoader = GameObject.FindWithTag("LevelLoader");
        doorOpenAudio = GetComponent<AudioSource>().clip;
    }

    private void Update()
    {
        checkClick();
    }

    private void checkClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "MenuDoor" && characterSelected)
                {
                    anim.SetBool("IsClicked", true);
                    doorOpened = true;
                    if (!soundEffectPlayed)
                    {
                        AudioSource.PlayClipAtPoint(doorOpenAudio, transform.position,0.5f);
                        soundEffectPlayed = true;
                    }
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player") && doorOpened)
        {
            levelLoader.GetComponent<LevelLoader>().LoadNextLevel();
            Invoke("setupGameStart", 1f);
            
            DontDestroyOnLoad(selectedCharacter);
            DontDestroyOnLoad(Camera.main.transform.parent);
        }
    }
    
    public void setupGameStart()
    {
        selectedCharacter.transform.position = new Vector3(-4f,-2.75f,0f);
        selectedCharacter.GetComponent<PlayerShootingManager>().enabled = true;
    }
}
