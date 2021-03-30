using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject
{
    [SerializeField] private int lives = 3;
    [SerializeField] private float maxSpeed = 7;
    [SerializeField] public float jumpSpeed = 7;
    [SerializeField] private GameObject tongue;
    
    private SpriteRenderer spriteRenderer;
    //private Animator animator;
    private UIManager uIManager;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //animator = GetComponent<Animator>();
        uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    protected override void FireTongue()
    {
        Vector3 tongue_length = new Vector3(2.0f, 0.0f, 0.0f);
        if( Input.GetKeyDown(KeyCode.T) ){
            Instantiate(tongue, transform.position + tongue_length, Quaternion.identity);
        }
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpSpeed;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
            {
                velocity.y *= 0.5f;
            }
        }

        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        //animator.SetBool("grounded", grounded);
        //animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;
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
}
