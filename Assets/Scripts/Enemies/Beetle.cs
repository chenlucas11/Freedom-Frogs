using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beetle : EnemyPhysics
{
    private bool flipped = false;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(BeetleRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (flipped)
        {
            targetVelocity = Vector2.zero;
        }
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
            if (!flipped)
            {
                FlipOver();
            }
        }
        else if (other.CompareTag("Projectile"))
        {
            if (flipped)
            {
                Destroy(other.gameObject);
                OnBeetleDeath();
            }
            else
            {
                FlipOver();
                Destroy(other.gameObject);
            }
        }
        else if (other.CompareTag("PlayerBottom"))
        {
            PlayerController player = other.transform.parent.GetComponent<PlayerController>();
            if (flipped)
            {
                player.Knockforward();
                OnBeetleDeath();
            }
        }
    }

    private void FlipOver()
    {
        if (!flipped)
        {
            this.transform.localScale = new Vector2(1, -1);
            animator.SetTrigger("OnBeetleFlip");
            flipped = true;
        }
    }

    IEnumerator BeetleRoutine()
    {
        while (!flipped)
        {
            targetVelocity = Vector2.right;
            if (flipped)
                yield break;
            yield return new WaitForSeconds(5.0f);
            targetVelocity = Vector2.zero;
            yield return new WaitForSeconds(2.0f);
            if (flipped)
                yield break;
            this.transform.localScale = new Vector2(1, 1);
            targetVelocity = Vector2.left;
            if (flipped)
                yield break;
            yield return new WaitForSeconds(5.0f);
            targetVelocity = Vector2.zero;
            yield return new WaitForSeconds(2.0f);
            if (flipped)
                yield break;
            this.transform.localScale = new Vector2(-1, 1);
        }
    }

    private void OnBeetleDeath()
    {
        this.GetComponent<Collider2D>().enabled = false;
        this.gravityModifier = 0;
        animator.SetTrigger("OnBeetleDeath");
        Destroy(this.gameObject, 1.3f);
    }
}
