using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int requiredKills;
    public bool isFinalDoor;

    public AudioClip levelDoorSE;

    public void playlevelDoorSE()
    {
        AudioSource.PlayClipAtPoint(levelDoorSE, transform.position, 1f);
    }
}