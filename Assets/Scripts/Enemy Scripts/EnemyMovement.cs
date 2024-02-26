using System;
using Pathfinding;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyMovement : MonoBehaviour
{
    /*[SerializeField] private float speed;

    private Rigidbody2D rb;*/

    private PlayerAwarenessController pac;
    private Vector2 targetDirection;

    private Animator anim;

    private Vector3 tempScale;

    private GameManager gm;

    private bool isDead;
    private bool hasBeenHit;
    private float lastHitTime = 0f;

    private PlayerMovement player;

    private Shake shake;

    [SerializeField] private float health = 50f;
    private HealthBar playerHealthBar;

    [SerializeField] private float monsterDamage;
    private float bulletDamage;


    //Damage PopUp
    public GameObject damagePopUp;
    private float randomRotation;
    private float randomX;

    private AIDestinationSetter aiDestinationSetter;
    private AIPath aiPath;

    public GameOverScreen gameOverScreen;
    
    public AudioClip enemyDeadSE;
    
    public AudioClip playerHitSE;
    
    //enemy shooting 
    /*public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float fireRate = 1f; // mermi atma hızı
    private float nextFire = 0.0f; // bir sonraki ateş etme zamanı*/

    private void Awake()
    {
        //rb = GetComponent<Rigidbody2D>();
        pac = GetComponent<PlayerAwarenessController>();
        anim = GetComponent<Animator>();
        gm = FindObjectOfType<GameManager>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();
        playerHealthBar = FindObjectOfType<HealthBar>();
        DontDestroyOnLoad(playerHealthBar.transform.parent);

        aiDestinationSetter = GetComponent<AIDestinationSetter>();
        aiPath = GetComponent<AIPath>();

        damagePopUp.GetComponentInChildren<MeshRenderer>().sortingLayerName = "UI";
        damagePopUp.GetComponentInChildren<MeshRenderer>().sortingOrder = 5;
        
        gameOverScreen = FindObjectOfType<GameOverScreen>();
    }

    private void FixedUpdate()
    {
        UpdateTargetDirection();
        //SetVelocity();
    }

    private void Update()
    {
        checkIfPlayerCanHitEnemy();
        targetDirection =
            pac.DirectionToPlayer; // A Vector between enemy and player. check its x value to handle Facing player
    }

    private void UpdateTargetDirection()
    {
        if (pac.AwareOfPlayer)
        {
            aiDestinationSetter.target = player.transform;
            anim.SetBool("Walk", true);
            //targetDirection = pac.DirectionToPlayer; //OLD BASIC AI
        }
        else
        {
            aiDestinationSetter.target = null;
            //targetDirection = Vector2.zero; //OLD BASIC AI
            anim.SetBool("Walk", false);
        }

        HandleEnemyTurning();
    }

    private void HandleEnemyTurning()
    {
        targetDirection.x = Mathf.RoundToInt(targetDirection.x); // 1.3 --> 1

        tempScale = transform.localScale;

        if (targetDirection.x > 0)
        {
            tempScale.x = Mathf.Abs(tempScale.x);
        }
        else if (targetDirection.x < 0)
        {
            tempScale.x = -Mathf.Abs(tempScale.x); //change enemy's scale for face the left side
        }

        transform.localScale = tempScale;
    }

    /*private void SetVelocity()
    {
        if (targetDirection == Vector2.zero)
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            rb.velocity = new Vector2(targetDirection.x, targetDirection.y) * speed;
        }
    }*/

    private void checkIfPlayerCanHitEnemy()
    {
        if (hasBeenHit &&
            Time.time - lastHitTime >= 0.1f) //for the bullet remains inside the monster's trigger for multiple frames
        {
            lastHitTime = Time.time;
            hasBeenHit = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Bullet"))
        {
            if (!isDead && !hasBeenHit)
            {
                hasBeenHit = true;
                anim.SetTrigger("Hit");
                bulletDamage = col.GetComponent<Bullet>().damageAmount;
                health -= bulletDamage; // bullet specified dmg
                displayDamagePopUp();
                col.gameObject.SetActive(false); //delete bullet
                //if dead
                if (health <= 0f)
                {
                    //speed = 0;
                    AudioSource.PlayClipAtPoint(enemyDeadSE, transform.position, 0.7f);
                    isDead = true;
                    aiPath.canMove = false;
                    gm.EnemyKilled();
                    anim.SetTrigger("Death");
                    shake.CamShakeKillEnemy();
                    Destroy(gameObject, 0.5f);
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (player.invincible == false)
            {
                AudioSource.PlayClipAtPoint(playerHitSE, transform.position, 1f);
                player.health -= monsterDamage;
                player.invincible = true;
                player.invincibilityTime = Time.time;
                
                //Debug.Log("hp: " + col.gameObject.GetComponent<PlayerMovement>().health);
                player.GetComponent<Animator>().SetTrigger("Damage");
                if (player.health <= 0)
                {
                    gameOverScreen.gameOver = true;
                }
            }
        }
    }

    private void displayDamagePopUp()
    {
        damagePopUp.GetComponentInChildren<TextMesh>().text = bulletDamage.ToString();

        //Random Algorithm Explanation: if display will be on left side text should be positive rotation, else text should be negative rotation

        randomX = Random.Range(-0.15f, 0.15f);
        Vector3 damagePopUpPosition = new Vector3(transform.position.x + randomX, transform.position.y + 0.15f,
            transform.position.z);

        if (randomX < 0)
        {
            randomRotation = Random.Range(0, 30);
        }
        else
        {
            randomRotation = Random.Range(-30, 0);
        }

        Quaternion damagePopUpRotation = Quaternion.Euler(0, 0, randomRotation);

        var damagePopUpCopy = Instantiate(damagePopUp, damagePopUpPosition, damagePopUpRotation);
        Destroy(damagePopUpCopy, 1f);
    }
}

