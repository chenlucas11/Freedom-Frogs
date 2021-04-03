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
    [SerializeField] private float force = 7;
    [SerializeField] private Vector3 tongue_length = new Vector3(2.5f, 0, 0);
    [SerializeField] private Vector3 projectileOffset = new Vector3(2f, 0, 0);
    private Rigidbody2D rigidBody2D;
    private GameObject tongue_instance;
    [SerializeField] private int currentBeat;
    [SerializeField] private float attackRate = 0.5f;
    private float canAttack = -1f;

    // Music Pieces
    [SerializeField] private int piecesCollected;

    [SerializeField] private Sprite[] idleSprites;
    [SerializeField] private Sprite[] jumpSprites;
    [SerializeField] private Sprite[] tongueAttackSprites;
    [SerializeField] private Sprite[] projectileAttackSprites;

    private UIManager uIManager;
    private SpriteRenderer playerSprite;
    //private Animator animator;

    void Awake()
    {
        uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        //animator = GetComponent<Animator>();
    }

    void Start()
    {
        canAttack = -1f;
        currentBeat = 0;
        piecesCollected = 0;
    }

    void Update()
    {
        CalcMovement();
        if (Input.GetKeyDown(KeyCode.T) && Time.time > canAttack && piecesCollected >= 2)
        {
            FireTongue();
        }
        else if (Input.GetKeyDown(KeyCode.R) && Time.time > canAttack && piecesCollected >= 3)
        {
            ShootProjectile();
        }
    }

    private void FireTongue()
    {
        canAttack = Time.time + attackRate;
        playerSprite.sprite = tongueAttackSprites[currentBeat];
        if (this.transform.localScale[0] < 1)
        {
            tongue_instance = (GameObject)Instantiate(tongue, transform.position - tongue_length, Quaternion.identity, transform);
        }
        else
        {
            tongue_instance = (GameObject)Instantiate(tongue, transform.position + tongue_length, Quaternion.identity, transform);
        }
        Destroy(tongue_instance, 0.2f);
    }

    private void ShootProjectile()
    {
        canAttack = Time.time + attackRate;
        playerSprite.sprite = projectileAttackSprites[currentBeat];
        if (this.transform.localScale[0] < 1)
        {
            Instantiate(projectile, transform.position - projectileOffset, Quaternion.identity);
        }
        else
        {
            Instantiate(projectile, transform.position + projectileOffset, Quaternion.identity);
        }
    }

    private void CalcMovement()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");

        if(piecesCollected == 0)
        {
            transform.Translate(new Vector2(horizontalMovement * speed * Time.deltaTime, 0));
        }

        if (Input.GetButtonDown("Jump") && piecesCollected >= 1)
        {
            rigidBody2D.AddForce(new Vector2(horizontalMovement * speed, jumpSpeed), ForceMode2D.Impulse);
            playerSprite.sprite = jumpSprites[currentBeat];
        }
        
        if (rigidBody2D.velocity.y < 0)
        {
            playerSprite.sprite = idleSprites[currentBeat];
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

        uIManager.UpdateLives(lives);

        if (lives < 1)
        {
            Destroy(this.gameObject);
        }
    }

    public void Knockback(Collision2D collision)
    {
        if (!hit)
        {
            ContactPoint2D contactPoint = collision.GetContact(0);
            Vector2 playerPosition = this.transform.position;
            Vector2 dir = contactPoint.point - playerPosition;
            dir = -dir.normalized;
            rigidBody2D.AddForce(dir * force, ForceMode2D.Impulse);
            StartCoroutine(ApplyFlash());
        }
    }

    IEnumerator ApplyFlash()
    {
        hit = true;
        for (int i = 0; i < 4; i++)
        {
            playerSprite.sprite = idleSprites[4];
            yield return new WaitForSeconds(0.1f);
            playerSprite.sprite = idleSprites[currentBeat];
            yield return new WaitForSeconds(0.1f);
        }
        playerSprite.sprite = idleSprites[4];
        yield return new WaitForSeconds(0.1f);
        playerSprite.sprite = idleSprites[currentBeat];
        hit = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MusicPiece"))
        {
            Destroy(other.gameObject);
            piecesCollected++;
            uIManager.UpdateMusicPieces(piecesCollected);
        }
    }
}

