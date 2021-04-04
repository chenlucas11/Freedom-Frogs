using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : EnemyPhysics
{
    [SerializeField] private float jumpSpeed = 15.0f;
    private bool falling = false;
    [SerializeField] private Sprite deathSprite;
    private SpriteRenderer fishSprite;

    // Start is called before the first frame update
    void Start()
    {
        fishSprite = GetComponent<SpriteRenderer>();
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
            player.Damage();
            player.Knockforward();
            OnFishDeath();
        }
        else if (other.CompareTag("Tongue"))
        {
            OnFishDeath();
        }
        else if (other.CompareTag("PlayerBottom"))
        {
            other.transform.parent.GetComponent<PlayerController>().Knockforward();
            OnFishDeath();
        }
        else if (other.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
            OnFishDeath();
        }
    }

    private void OnFishDeath()
    {
        this.GetComponent<Collider2D>().enabled = false;
        this.gravityModifier = 0;
        this.velocity.y = 0;
        fishSprite.sprite = deathSprite;
        Destroy(this.gameObject, 1.0f);
    }
}
