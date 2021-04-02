using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beetle : PhysicsObject
{
    private bool flipped = false;
    [SerializeField] private Sprite[] beetleSprites;

    private void Start()
    {
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
            player.Knockback(collision);
            player.Damage();
        }
        else if (collision.gameObject.CompareTag("Tongue"))
        {
            Debug.Log("Tongue hit");
            if (flipped)
            {
                Destroy(this.gameObject);
            }
            else
            {
                FlipOver();
            }
        }
        else if (collision.gameObject.CompareTag("PlayerBottom"))
        {
            if (flipped)
            {
                Destroy(this.gameObject);
            }
            else
            {
                PlayerController player = collision.transform.parent.GetComponent<PlayerController>();
                player.Knockback(collision);
                player.Damage();
            }
        }
    }

    private void FlipOver()
    {
        if (!flipped)
        {
            this.transform.localScale = new Vector2(1, -1);
            this.GetComponent<SpriteRenderer>().sprite = beetleSprites[0];
            flipped = true;
        }
        else
        {
            this.transform.localScale = new Vector2(1, 1);
            this.GetComponent<SpriteRenderer>().sprite = beetleSprites[1];
            flipped = false;
        }
    }

    IEnumerator BeetleRoutine()
    {
        while (!flipped)
        {
            targetVelocity = Vector2.right;
            if (flipped)
                yield break;
            yield return new WaitForSeconds(3.0f);
            targetVelocity = Vector2.zero;
            yield return new WaitForSeconds(2.0f);
            if (flipped)
                yield break;
            this.transform.localScale = new Vector2(1, 1);
            targetVelocity = Vector2.left;
            if (flipped)
                yield break;
            yield return new WaitForSeconds(3.0f);
            targetVelocity = Vector2.zero;
            yield return new WaitForSeconds(2.0f);
            if (flipped)
                yield break;
            this.transform.localScale = new Vector2(-1, 1);
        }
    }
}
