using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StinkBeetle : EnemyPhysics
{
    [SerializeField] private Sprite[] stinkBeetleSprites;
    private bool dead = false;
    [SerializeField] private float attackRate = 3f;
    private float canAttack = -1f;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Vector3 projectileOffset = new Vector3(2f, 0, 0);

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(StinkBeetleRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > canAttack)
        {
            Attack();
        }
        else if (canAttack - Time.time < 1f)
        {
            this.GetComponent<SpriteRenderer>().sprite = stinkBeetleSprites[0];
        }
    }

    private void Attack()
    {
        canAttack = Time.time + attackRate;
        this.GetComponent<SpriteRenderer>().sprite = stinkBeetleSprites[0];
        Instantiate(projectile, transform.position - projectileOffset, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.transform.GetComponent<PlayerController>();
            player.Damage();
            player.Knockback(collision);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Tongue"))
        {
            OnStinkBeetleDeath();
        }
        else if (other.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
            OnStinkBeetleDeath();
        }
        else if (other.CompareTag("PlayerBottom"))
        {
            PlayerController player = other.transform.parent.GetComponent<PlayerController>();
            player.Knockforward();
            OnStinkBeetleDeath();
        }
    }

    IEnumerator StinkBeetleRoutine()
    {
        while (!dead)
        {
            yield return new WaitForSeconds(4.0f);
        }
    }

    private void OnStinkBeetleDeath()
    {
        dead = true;
        animator.SetTrigger("OnStinkBeetleDeath");
        this.GetComponent<Collider2D>().enabled = false;
        this.gravityModifier = 0;
        Destroy(this.gameObject, 1.3f);
    }
}
