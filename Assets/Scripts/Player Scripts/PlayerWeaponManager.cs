using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    [SerializeField] private WeaponManager[] playerWeapons;

    public int weaponIndex;

    [SerializeField] public GameObject[] weaponBullets;

    private Vector2 targetPos;

    private Vector2 direction;

    private Camera mainCam;

    private Vector2 bulletSpawnPosition;

    private Quaternion bulletRotation;
    
    private void Awake()
    {
        weaponIndex = 0;
        playerWeapons[weaponIndex].gameObject.SetActive(true);

        mainCam = Camera.main;
    }

    private void Update()
    {
        ChangeWeapon();
    }

    public void ActivateGun(int gunIndex)
    {
        playerWeapons[weaponIndex].ActivateGun(gunIndex); //weapon's side, down etc..
    }

    void ChangeWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerWeapons[weaponIndex].gameObject.SetActive(false);

            weaponIndex++;

            if (weaponIndex == playerWeapons.Length)
            {
                weaponIndex = 0;
            }

            playerWeapons[weaponIndex].gameObject.SetActive(true);
        }
    }

    public void Shoot(Vector3 spawnPos)
    {
        targetPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        
        bulletSpawnPosition = new Vector2(spawnPos.x, spawnPos.y);

        direction = (targetPos - bulletSpawnPosition).normalized;

        bulletRotation =
            Quaternion.Euler(0, 0,
                Mathf.Atan2(direction.y, direction.x) *
                Mathf.Rad2Deg); //atan2: calculate angle between two points in radian, Rad2Deg: Radians to degree

        GameObject newBullet = Instantiate(weaponBullets[weaponIndex], spawnPos, bulletRotation);

        newBullet.GetComponent<Bullet>().MoveInDirection(direction);
        newBullet.GetComponent<Bullet>().playSoundEffect();
    }
}