using Unity.VisualScripting;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public GameObject[] characters;

    private int testCharacterIndex = -1;
    private bool isTestCharacterSelected;
    private Camera mainCam;

    private GameObject mainDoor;
    
    public AudioClip[] clickSounds;
    
    private void Start()
    {
        mainCam = Camera.main;
        for (int i = 0; i < characters.Length; i++) 
        {
            characters[i].GetComponent<PlayerMovement>().enabled = false;
        }
        
        /*characters[testCharacterIndex].GetComponent<PlayerMovement>().enabled = true;
        mainCam.gameObject.GetComponentInParent<CameraFollow>().playerTarget = characters[testCharacterIndex].transform; //set camera follow for 1st character*/
        mainDoor = GameObject.FindWithTag("MenuDoor");
        
    }

    void Update()
    {
        //Detect when a character is clicked
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                if (isTestCharacterSelected && hit.collider.gameObject == characters[testCharacterIndex])      //If clicked on the test character
                { 
                    SelectCharacter();
                }
                else                                                                //To test another character
                {
                    TestCharacter(hit);
                }
            }
        }
    }

    void TestCharacter(RaycastHit2D hit)
    {
        for (int i = 0; i < characters.Length; i++)
        {
            if (hit.collider.gameObject == characters[i])
            {
                if (testCharacterIndex != -1) //If its first choosing section, don't reset
                {
                    ResetOldTestCharacter();                                                                                         //Default main menu character           
                }
                
                characters[i].GetComponent<PlayerMovement>().enabled = true;                                                     //enable new test character's movement
                characters[i].GetComponent<PlayerWeaponManager>().enabled = true;
                characters[i].transform.GetChild(0).gameObject.SetActive(true);                                                  //enable new test character's cursor
                
                testCharacterIndex = i;                                                                                          //set new character as a test character
                mainCam.gameObject.GetComponentInParent<CameraFollow>().playerTarget = characters[testCharacterIndex].transform; //set camera follow for test character
                isTestCharacterSelected = true;
                AudioSource.PlayClipAtPoint(clickSounds[0], transform.position, 0.4f);
            }
        }
    }

    void ResetOldTestCharacter()
    {
        characters[testCharacterIndex].GetComponent<PlayerMovement>().enabled = false;                                  //disable old test character's movement
        characters[testCharacterIndex].GetComponent<PlayerWeaponManager>().enabled = false;                             //disable old test character's weapon 
        characters[testCharacterIndex].transform.GetChild(0).gameObject.SetActive(false);                               //disable old test character's cursor
                    
        characters[testCharacterIndex].GetComponent<Animator>().SetFloat("FaceX", 0f);                                  //Make look down old test character / character reset
        characters[testCharacterIndex].GetComponent<Animator>().SetFloat("FaceY", -1f);                                 //Make look down old test character / character reset
        
        characters[testCharacterIndex].GetComponent<PlayerMovement>().ActivateWeaponForDirection(0f,-1f);               //gun direction to down
    }
    void SelectCharacter()
    {
        if (isTestCharacterSelected)
        {
            for (int i = 0; i < characters.Length; i++)
            {
                if (i != testCharacterIndex) //Deactivate all characters except selected character
                { 
                    characters[i].SetActive(false);
                }
            }
            mainCam.gameObject.GetComponentInParent<CameraFollow>().playerTarget = characters[testCharacterIndex].transform;
            mainDoor.gameObject.GetComponent<OpenDoor>().characterSelected = true;
            mainDoor.gameObject.GetComponent<OpenDoor>().selectedCharacter = characters[testCharacterIndex];
            characters[testCharacterIndex].transform.GetChild(0).gameObject.SetActive(false);                           //disable character's cursor
            AudioSource.PlayClipAtPoint(clickSounds[1], transform.position, 0.6f);
        }
    }
    
}