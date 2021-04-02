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
    private float projectileSpeed = 7;
    private Rigidbody2D rigidBody2D;

    

    private UIManager uIManager;
    //private Animator animator;

    void Awake()
    {
        uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
    }

    void Update()
    {
        CalcMovement();
        FireTongue();
        ShootProjectile();
    }

    private void FireTongue()
    {
        Vector3 tongue_length;
        if (this.transform.localScale[0] < 1)
        {
            tongue_length = new Vector3(-3.8f, 0.0f, 0.0f);
        }
        else
        {
            tongue_length = new Vector3(3.8f, 0.0f, 0.0f);
        }
        Quaternion rotation = Quaternion.LookRotation(Vector3.right);
        if (Input.GetKeyDown(KeyCode.T))
        {
            GameObject tongue_instance = (GameObject)Instantiate(tongue, transform.position + tongue_length, Quaternion.identity, transform);
            Destroy(tongue_instance, 1.0f);
        }
    }

    private void ShootProjectile()
    {
        if( Input.GetKeyDown(KeyCode.R) ){
            Vector3 projectile_length;
            if (this.transform.localScale[0] < 1)
            {
                projectile_length = new Vector3(-1.5f, 0.0f, 0.0f);
            }
            else
            {
                projectile_length = new Vector3(1.5f, 0.0f, 0.0f);
            }
            Instantiate(projectile, transform.position + projectile_length, Quaternion.identity);
            //Destroy(projectile_instance, 1.0f);
        }
    }

    private void CalcMovement()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            rigidBody2D.AddForce(new Vector2(move.x * speed, jumpSpeed), ForceMode2D.Impulse);
        }

        if (move.x > 0f)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (move.x < 0f)
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

