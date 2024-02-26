using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D myBody;

    [SerializeField] 
    private float moveSpeed = 2.5f;

    [SerializeField] 
    public float damageAmount;

    private bool dealthDamage; //Got damage

    [SerializeField] 
    public float deactivateTimer = 0.8f;

    [SerializeField] 
    
    private bool destroyObj;

    public AudioClip soundEffect;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();

        //get anim
        Invoke("DeactivateBullet", deactivateTimer); //invoke function in deactivateTimer seconds

        soundEffect = GetComponent<AudioSource>().clip;

    }

    public void MoveInDirection(Vector3 direction)
    {
        myBody.velocity = direction * moveSpeed;
    }
    
    void DeactivateBullet()
    {
        Destroy(gameObject);
        /*if (destroyObj)
        {
            
        }
        else
        {
            gameObject.SetActive(false);
        }*/
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TagManager.ENEMY_TAG) || 
            col.CompareTag(TagManager.SHOOTER_ENEMY_TAG) || 
            col.CompareTag(TagManager.BOSS_TAG))
        {
            
        }

        if (col.CompareTag(TagManager.BLOCKING_TAG))
        {
            
        }
    }

    public void playSoundEffect()
    {
        AudioSource.PlayClipAtPoint(soundEffect, transform.position);
    }
}
