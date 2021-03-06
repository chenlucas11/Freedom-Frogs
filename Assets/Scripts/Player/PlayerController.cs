using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool hit = false;
    [SerializeField] private int lives = 3;
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpSpeed = 8;
    [SerializeField] private GameObject tongue;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float force = 4;
    [SerializeField] private Vector3 tongue_length = new Vector3(2.5f, 0, 0);
    [SerializeField] private Vector3 projectileOffset = new Vector3(2f, 0, 0);
    private Rigidbody2D rigidBody2D;
    private GameObject tongue_instance;
    [SerializeField] private float attackRate = 0.5f;
    private float canAttack = -1f;
    private bool hasJumped = false;
    private float horizontalForce = 0;
    [SerializeField] private float horizontalJumpIncrement = 0.3f;
    [SerializeField] private int horizontalJumpNum = 0;
    [SerializeField] private int piecesCollected;
    private float canCollectPiece = -1f;
    private const float collectCD = 5f;
    private float canCollectLife = -1f;
    public bool grounded = true;
    private bool tongueOut = false;
    private bool projectileOut = false;
    private bool tutorialOut = false;
    private Vector3 checkpoint;

    [SerializeField] private Sprite[] idleSprites;
    [SerializeField] private Sprite[] jumpSprites;
    [SerializeField] private Sprite[] tongueAttackSprites;
    [SerializeField] private Sprite[] projectileAttackSprites;

    private UIManager uIManager;
    private SpriteRenderer playerSprite;
    private Conductor conductor;
    private AudioManager audioManager;
    private GameManager gameManager;


    //private Animator animator;

    void Awake()
    {
        uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        conductor = GameObject.Find("Conductor").GetComponent<Conductor>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        //animator = GetComponent<Animator>();
    }

    void Start()
    {
        canAttack = -1f;
        piecesCollected = 0;
        tongueOut = false;
        projectileOut = false;
        tutorialOut = false;
    }

    void Update()
    {
        CalcMovement();
        if (Input.GetKeyDown(KeyCode.E) && Time.time > canAttack && piecesCollected >= 2)
        {
            FireTongue();
        }
        else if (Input.GetKeyDown(KeyCode.Q) && Time.time > canAttack && piecesCollected >= 3)
        {
            ShootProjectile();
        }
    }

    private void FireTongue()
    {
        canAttack = Time.time + attackRate;
        playerSprite.sprite = tongueAttackSprites[conductor.beatNum % 4];
        if (this.transform.localScale[0] < 1)
        {
            tongue_instance = (GameObject)Instantiate(tongue, transform.position - tongue_length, Quaternion.identity, transform);
        }
        else
        {
            tongue_instance = (GameObject)Instantiate(tongue, transform.position + tongue_length, Quaternion.identity, transform);
        }
        StartCoroutine(TongueRoutine());
        Destroy(tongue_instance, 0.5f);
    }

    private void ShootProjectile()
    {
        canAttack = Time.time + attackRate;
        playerSprite.sprite = projectileAttackSprites[conductor.beatNum % 4];
        if (this.transform.localScale[0] < 1)
        {
            Instantiate(projectile, transform.position - projectileOffset, Quaternion.identity);
        }
        else
        {
            Instantiate(projectile, transform.position + projectileOffset, Quaternion.identity);
        }
        StartCoroutine(ProjectileRoutine());
    }

    private void CalcMovement()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");

        if (piecesCollected == 0)
        {
            transform.Translate(new Vector2(horizontalMovement * speed * Time.deltaTime, 0));
        }

        if (piecesCollected >= 1)
        {
            if (!tongueOut && !projectileOut)
            {
                playerSprite.sprite = idleSprites[conductor.beatNum % 4];
            }

            if (horizontalJumpNum < 3)
            {
                if (Input.GetKeyDown("right") || Input.GetKeyDown(KeyCode.D))
                {
                    horizontalForce += horizontalJumpIncrement;
                    horizontalJumpNum++;
                    uIManager.UpdateArrows(horizontalJumpNum, 2);
                }
                else if (Input.GetKeyDown("left") || Input.GetKeyDown(KeyCode.A))
                {
                    horizontalForce -= horizontalJumpIncrement;
                    horizontalJumpNum++;
                    uIManager.UpdateArrows(horizontalJumpNum, 1);
                }
            }

            if (conductor.beatNum % 4 == 0 && !hasJumped)
            {
                if (grounded)
                {
                    rigidBody2D.AddForce(new Vector2(horizontalForce * speed, jumpSpeed), ForceMode2D.Impulse);
                    if (!tongueOut && !projectileOut)
                        playerSprite.sprite = jumpSprites[conductor.beatNum % 4];
                    else if (tongueOut)
                    {
                        playerSprite.sprite = tongueAttackSprites[conductor.beatNum % 4];
                    }
                    else if (projectileOut)
                    {
                        playerSprite.sprite = projectileAttackSprites[conductor.beatNum % 4];
                    }
                }
                hasJumped = true;
                horizontalForce = 0;
                horizontalJumpNum = 0;
                uIManager.UpdateArrows(horizontalJumpNum, 0);
            }
            else if (conductor.beatNum % 4 == 3 && hasJumped)
                hasJumped = false;

            if (!tongueOut && !projectileOut)
            {
                if (grounded)
                {
                    playerSprite.sprite = idleSprites[conductor.beatNum % 4];
                }
                else
                {
                    playerSprite.sprite = jumpSprites[conductor.beatNum % 4];
                }
            }
            else if (tongueOut)
            {
                playerSprite.sprite = tongueAttackSprites[conductor.beatNum % 4];
            }
            else if (projectileOut)
            {
                playerSprite.sprite = projectileAttackSprites[conductor.beatNum % 4];
            }
        }

        if (horizontalMovement > 0f)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (horizontalMovement < 0f)
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }

    public void Damage()
    {
        lives--;
        if (lives < 1)
        {
            lives = 3;
            transform.position = gameManager.lastCheckpoint;
        }
        uIManager.UpdateLives(lives);
    }

    public void Knockback(Collision2D collision)
    {
        if(lives > 0)
        {
            ContactPoint2D contactPoint = collision.GetContact(0);
            Vector2 playerPosition = this.transform.position;
            Vector2 dir = contactPoint.point - playerPosition;
            dir = -dir.normalized;
            if (dir.x > 0)
                dir = new Vector2(1, 1);
            else
                dir = new Vector2(-1, 1);
            rigidBody2D.velocity = new Vector2(0, 0);
            rigidBody2D.AddForce(dir * force, ForceMode2D.Impulse);
        }
    }

    public void Knockforward()
    {
        if (lives > 0)
        {
            rigidBody2D.velocity = new Vector2(0, 0);
            rigidBody2D.AddForce(new Vector2(0.8f, 1.3f) * force, ForceMode2D.Impulse);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MusicPiece"))
        {
            if (Time.time > canCollectPiece)
            {
                gameManager.lastCheckpoint = other.transform.position;
                Destroy(other.gameObject);
                piecesCollected++;
                if (piecesCollected == 1 && !tutorialOut)
                {
                    uIManager.UpdateTutorial();
                    tutorialOut = true;
                }
                uIManager.UpdateMusicPieces(piecesCollected);
                audioManager.UpdateAudio(piecesCollected);
                canCollectPiece = Time.time + collectCD;
            }
        }
        else if (other.CompareTag("Life"))
        {
            if (Time.time > canCollectLife)
            {
                Destroy(other.gameObject);
                if (lives < 3)
                {
                    lives++;
                    uIManager.UpdateLives(lives);
                }
                canCollectLife = Time.time + collectCD;
            }
        }
        else if (other.CompareTag("Deadzone"))
        {
            lives = 1;
            Damage();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        ContactPoint2D contactPoint = collision.GetContact(0);
        Vector2 playerPosition = this.transform.position;
        if (playerPosition.y > contactPoint.point.y)
            grounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        grounded = false;
    }

    IEnumerator TongueRoutine()
    {
        tongueOut = true;
        yield return new WaitForSeconds(attackRate);
        tongueOut = false;
    }

    IEnumerator ProjectileRoutine()
    {
        projectileOut = true;
        yield return new WaitForSeconds(attackRate);
        projectileOut = false;
    }
}

