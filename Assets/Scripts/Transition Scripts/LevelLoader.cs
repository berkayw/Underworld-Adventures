using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator anim;
    private float waitTime = 1.5f;

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public IEnumerator LoadLevel(int levelIndex)
    {
        anim.SetTrigger("Start");
        
        yield return new WaitForSeconds(waitTime);
        
        SceneManager.LoadScene(levelIndex);
    }
    
    
}
