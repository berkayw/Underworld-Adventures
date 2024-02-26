using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private GameObject[] guns;

    private int currentGun;

    private void Start()
    {
        DeactivateAllGuns();
    }

    public void DeactivateAllGuns()
    {
        for (int i = 0; i < guns.Length; i++)
        {
            guns[i].SetActive(false);
        }
    }

    public void ActivateGun(int newGunIndex)
    {
        guns[currentGun].SetActive(false);
        guns[newGunIndex].SetActive(true);
        currentGun = newGunIndex;
    }
    
} 