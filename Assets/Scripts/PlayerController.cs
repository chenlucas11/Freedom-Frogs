using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject
{
    [SerializeField] private int lives = 3;
    [SerializeField] private float maxSpeed = 3;
    [SerializeField] private float jumpSpeed = 10;
    [SerializeField] private GameObject tongue;

    // Music Pieces
    //[SerializeField] private bool pieceOneCollected = false;
    //[SerializeField] private bool pieceTwoCollected = false;
    //[SerializeField] private bool pieceThreeCollected = false;

    private UIManager uIManager;
    //private Animator animator;

    void Awake()
    {
        uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        //animator = GetComponent<Animator>();
    }

    void Update()
    {
        ComputeVelocity();
        FireTongue();
    }

    private void FireTongue()
    {
        Vector3 tongue_length = new Vector3(3.8f, 0.0f, 0.0f);
        Quaternion rotation = Quaternion.LookRotation(Vector3.right);
        if( Input.GetKeyDown(KeyCode.T) ){
            GameObject tongue_instance = (GameObject) Instantiate(tongue, transform.position + tongue_length, Quaternion.identity, transform);
            Destroy(tongue_instance,1.0f);
        }
    }

    private void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpSpeed;
        }

        if (move.x > 0f)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (move.x < 0f)
        {
            transform.localScale = new Vector2(-1, 1);
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
