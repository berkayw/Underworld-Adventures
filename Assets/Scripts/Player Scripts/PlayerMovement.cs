using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : CharacterMovement
{
    private float moveX, moveY;

    private Camera mainCam;

    private Vector2 mousePosition;
    private Vector2 direction;
    private Vector3 tempScale;

    private Animator anim;
    
    public float maxHealth = 100f;
    public float health;
    
    
    public bool invincible;
    public float invincibilityTime;
    private const float INVINCIBILITY_DURATION = 2f;

    private PlayerWeaponManager playerWeaponManager;
    
    public void Awake()
    {
        base.Awake();

        mainCam = Camera.main;

        anim = GetComponent<Animator>();

        playerWeaponManager = GetComponent<PlayerWeaponManager>();

        health = maxHealth;
        
        
    }

    private void Update()
    {
        checkInvincibility();
    }
    
    private void FixedUpdate()
    {
        moveX = Input.GetAxisRaw(TagManager.HORIZONTAL_AXIS); //raw means only return 0, 1, -1.
        moveY = Input.GetAxisRaw(TagManager.VERTICAL_AXIS);

        HandlePlayerTurning(); // rotate the player based on where mouse is

        HandleMovement(moveX, moveY);
    }

    private void checkInvincibility()
    {
        if (invincible && Time.time - invincibilityTime >= INVINCIBILITY_DURATION)
        {
            invincible = false;
        }
    }
    void HandlePlayerTurning()
    {
        mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);

        direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y)
            .normalized; // need to normalize to get only direction. dont need magnitude of vector

        HandlePlayerAnimation(direction.x, direction.y);
    }

    void HandlePlayerAnimation(float x, float y)
    {
        x = Mathf.RoundToInt(x); // 1.3 --> 1
        y = Mathf.RoundToInt(y);

        tempScale = transform.localScale;
        if (x > 0)
        {
            tempScale.x = Mathf.Abs(tempScale.x);
        }
        else if (x < 0)
        {
            tempScale.x = -Mathf.Abs(tempScale.x); //change player's scale for face the left side
        }

        transform.localScale = tempScale;

        x = Mathf.Abs(x); // x is always positive for all side animations

        anim.SetFloat(TagManager.FACE_X_ANIMATION_PARAMETER, x);
        anim.SetFloat(TagManager.FACE_Y_ANIMATION_PARAMETER, y);

        ActivateWeaponForDirection(x, y);
    }

    public void ActivateWeaponForDirection(float x, float y)
    {
        //side
        if (x == 1f && y == 0f)
        {
            playerWeaponManager.ActivateGun(0);
        }

        //up
        if (x == 0f && y == 1f)
        {
            playerWeaponManager.ActivateGun(1);
        }

        //down
        if (x == 0f && y == -1f)
        {
            playerWeaponManager.ActivateGun(2);
        }

        //side up
        if (x == 1f && y == 1f)
        {
            playerWeaponManager.ActivateGun(3);
        }

        //side down
        if (x == 1f && y == -1f)
        {
            playerWeaponManager.ActivateGun(4);
        }
    }

}