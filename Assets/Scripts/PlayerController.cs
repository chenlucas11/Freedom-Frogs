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
    [SerializeField] private Vector3 tongue_length = new Vector3(2.5f, 0.0f, 0.0f);
    [SerializeField] private Vector3 projectileOffset = new Vector3(1.5f, 0.0f, 0.0f);
    private Rigidbody2D rigidBody2D;
    private GameObject tongue_instance;

    private float canAttack = -1f;
    [SerializeField] private float attackRate = 0.5f;


    private UIManager uIManager;
    //private Animator animator;

    void Awake()
    {
        uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
    }

    void Start()
    {
        canAttack = -1f;
    }

    void Update()
    {
        CalcMovement();
        if (Input.GetKeyDown(KeyCode.T) && Time.time > canAttack)
        {
            FireTongue();
        }
        else if (Input.GetKeyDown(KeyCode.R) && Time.time > canAttack)
        {
            ShootProjectile();
        }
    }

    private void FireTongue()
    {
        canAttack = Time.time + attackRate;
        if (this.transform.localScale[0] < 1)
        {
            tongue_instance = (GameObject)Instantiate(tongue, transform.position - tongue_length, Quaternion.identity, transform);
        }
        else
        {
            tongue_instance = (GameObject)Instantiate(tongue, transform.position + tongue_length, Quaternion.identity, transform);
        }
        Destroy(tongue_instance, 0.5f);
    }

    private void ShootProjectile()
    {
        canAttack = Time.time + attackRate;
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

        if (Input.GetButtonDown("Jump"))
        {
            rigidBody2D.AddForce(new Vector2(horizontalMovement * speed, jumpSpeed), ForceMode2D.Impulse);
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
            StartCoroutine(ApplyKnockback(dir));
        }
    }

    IEnumerator ApplyKnockback(Vector2 dir)
    {
        hit = true;
        yield return new WaitForSeconds(0.5f);
        hit = false;
    }
}

