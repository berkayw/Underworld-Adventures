using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingManager : MonoBehaviour
{
    [SerializeField] private float shootingTimerLimit = 0.2f; // cooldown
    private float shootingTimer;

    [SerializeField] private Transform bulletSpawnPos;

    private PlayerWeaponManager playerWeaponManager;

    private Shake shake;

    private Animator muzzleFlashAnim;
    private GameObject muzzleFlash;
    private void Awake()
    {
        playerWeaponManager = GetComponent<PlayerWeaponManager>();
        bulletSpawnPos =
            GameObject.FindWithTag(TagManager.PLAYER_TAG).transform.GetChild(1)
                .transform; //Access Bullet spawn position's transform
        
        
        muzzleFlashAnim = bulletSpawnPos.GetChild(0).GetComponent<Animator>();
        muzzleFlash = bulletSpawnPos.GetChild(0).gameObject;
        
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();
    }

    private void Update()
    {
        HandleShooting();
    }

    void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time > shootingTimer)
            {
                shootingTimer = Time.time + shootingTimerLimit;
                //animate muzzle flash

                CreateBullet();
            }
        }
    }

    void CreateBullet()
    {
        if (bulletSpawnPos == null)
        {
            Awake();
        }
        else
        {
            playerWeaponManager.Shoot(bulletSpawnPos.position);
            shake.CamShakeRandom();
            muzzleFlash.SetActive(true);
            muzzleFlashAnim.SetTrigger("shoot");
            Invoke("DeactivateMuzzleFlash", 0.6f);
        }

    }
    
    void DeactivateMuzzleFlash()
    {
        muzzleFlash.SetActive(false);
    }
}