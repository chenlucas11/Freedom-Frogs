using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : EnemyPhysics
{
    [SerializeField] private float jumpSpeed = 15.0f;
    private bool falling = false;

    // Start is called before the first frame update
    void Start()
    {
        velocity.y = jumpSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (velocity.y == 0 && falling == true)
        {
            transform.Rotate(0.0f, 0.0f, -180.0f);
            velocity.y = jumpSpeed;
            falling = false;
        }
        if (velocity.y < 0 && falling == false)
        {
            transform.Rotate(0.0f, 0.0f, 180.0f);
            falling = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.transform.GetComponent<PlayerController>();

            if (player != null)
                player.Damage();
        }
        else if (other.CompareTag("Tongue"))
        {
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("PlayerBottom"))
        {
            Destroy(this.gameObject);
        }
    }
}
